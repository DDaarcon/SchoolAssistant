type ValidationFail<T> = {
    error?: 'null' | 'empty' | 'invalidDate' | string;
    on?: keyof T;
}

type Rules<T, TProp extends keyof T> = {
    notNull?: boolean | string;
    notEmpty?: boolean | string;
    validDate?: boolean | string;
    other?: ((model: T, prop: TProp) => ValidationFail<T>[] | ValidationFail<T> | undefined)[]
}

type RulesForModel<T extends {}> = {
    [index in keyof T]?: Rules<T, index>;
};

class Validator<T extends {}> {
    private _model?: T;
    private _modelGetter?: () => T;

    private _rules?: RulesForModel<T>;

    private _errors?: ValidationFail<T>[];

    get errors() { return this._errors ?? []; }

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

            if (rules.notNull)
                if (!this.validateNotNull(prop))
                    continue;

            if (rules.notEmpty)
                if (!this.validateNotEmpty(prop))
                    continue;

            if (rules.validDate)
                if (!this.validateDate(prop))
                    continue;

            for (const otherRule of rules.other ?? []) {
                const otherErrors = otherRule(this._model, prop);
                if (!otherErrors)
                    continue;

                if (otherErrors instanceof Array)
                    this._errors.push(...otherErrors);
                else
                    this._errors.push(otherErrors);
            }
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
        if (!val) return true;

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