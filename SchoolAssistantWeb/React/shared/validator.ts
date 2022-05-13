type ValidationMethod<T, TProp extends keyof T> = (model: T, prop: TProp) => ValidationFail<TProp>[] | ValidationFail<TProp> | undefined;

type SubValidators<T> = {
    [index in keyof T]?: Validator<T[index]>;
}


export type ValidationFail<TProp> = {
    error?: 'null' | 'empty' | 'invalidDate' | string;
    on?: TProp;
}




export type Rules<T, TProp extends keyof T> = {
    notNull?: boolean | string;
    notEmpty?: boolean | string;
    validDate?: boolean | string;
    other?: ValidationMethod<T, TProp>[] | ValidationMethod<T, TProp>
    subValidator?: (getModel: () => T, prop: TProp) => RulesForModel<T[TProp]>;
}

export type RulesForModel<T extends {}> = {
    [index in keyof T]?: Rules<T, index>;
};

export default class Validator<T extends {}> {
    private _model?: T;
    private _modelGetter?: () => T;

    private _rules?: RulesForModel<T>;

    private _errors?: ValidationFail<keyof T>[];
    private _subValidators: SubValidators<T> = {};

    get errors() { return this._errors ?? []; }

    getErrorMsgsFor(prop: keyof T) {
        return this.errors.filter(x => x.on == prop).map(x => x.error);
    }

    get subValidators() { return this._subValidators; }


    forModel(model: T) {
        this._model = model;
        this._modelGetter = undefined;
    }

    forModelGetter(getter: () => T) {
        this._model = undefined;
        this._modelGetter = getter;
    }

    setRules(rules: RulesForModel<T>) {
        this._rules = rules;
        this._subValidators = {};
    }

    validate(): boolean {
        if (this._modelGetter) this._model = this._modelGetter();
        if (!this._model)
            return false;

        this._errors = [];

        const properties = Object.keys(this._model) as unknown as (keyof T)[];

        for (const prop of properties) {
            if (!this._rules[prop])
                continue;

            const rules = this._rules[prop];

            if (rules.notNull && !this.validateNotNull(prop))
                continue;

            if (rules.notEmpty && !this.validateNotEmpty(prop))
                continue;

            if (rules.validDate && !this.validateDate(prop))
                continue;

            if (rules.other)
                this.validateOther(prop, rules.other);

            if (rules.subValidator)
                this.useSubValidator(prop, rules.subValidator);
        }

        return this._errors.length == 0;
    }

    private validateNotNull(prop: keyof T): boolean {
        if (this._model[prop] != undefined && this._model[prop] != null)
            return true;

        this.addError(prop, 'null', 'notNull');
        return false;
    }

    private validateNotEmpty(prop: keyof T): boolean {
        const val = this._model[prop];
        if (val == undefined) return true;

        if (val['length']) return true;

        this.addError(prop, 'empty', 'notEmpty');
        return false;
    }

    private validateDate(prop: keyof T): boolean {
        const val = this._model[prop];
        if (!val) return true;

        if (val instanceof Date) return true;
        if (typeof val === 'string' || !isNaN(Date.parse(val as unknown as string))) return true;

        this.addError(prop, 'invalidDate', 'validDate');
        return false;
    }



    private validateOther(prop: keyof T, other: ValidationMethod<T, keyof T>[] | ValidationMethod<T, keyof T>) {
        if (other instanceof Array) {
            for (const otherRule of other ?? []) {
                const otherErrors = otherRule(this._model, prop);
                if (!otherErrors)
                    continue;

                if (otherErrors instanceof Array)
                    this._errors.push(...otherErrors);
                else
                    this._errors.push(otherErrors);
            }
        }
        else {
            const otherError = other?.(this._model, prop);
            if (otherError) {
                if (otherError instanceof Array)
                    this._errors.push(...otherError);
                else
                    this._errors.push(otherError);
            }
        }
    }

    private useSubValidator(prop: keyof T, getRules: (getModel: () => T, prop: keyof T) => RulesForModel<T[keyof T]>) {
        if (!this._subValidators[prop]) {
            this._subValidators[prop] = new Validator<T[keyof T]>();
            this._subValidators[prop].setRules(
                getRules(() => this._model, prop)
            )
        }

        this._subValidators[prop].forModel(this._model[prop]);
        this._subValidators[prop].validate();
    }



    private addError<TProp extends keyof T>(prop: TProp, defaultMsg: string, rulesProp?: keyof Rules<T, TProp>) {
        let error;
        if (typeof this._rules[prop][rulesProp] === 'string')
            error = this._rules[prop][rulesProp];
        else
            error = defaultMsg;

        this._errors.push({
            on: prop,
            error: error
        });
    }
}