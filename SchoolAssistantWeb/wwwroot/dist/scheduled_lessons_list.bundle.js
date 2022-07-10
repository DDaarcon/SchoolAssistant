"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["scheduled_lessons_list"],{

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.css":
/*!************************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.css ***!
  \************************************************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.css":
/*!*******************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.css ***!
  \*******************************************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/components/list-body.css":
/*!***************************************************************!*\
  !*** ./React/scheduled-lessons-list/components/list-body.css ***!
  \***************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/components/list-head.css":
/*!***************************************************************!*\
  !*** ./React/scheduled-lessons-list/components/list-head.css ***!
  \***************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/components/row.css":
/*!*********************************************************!*\
  !*** ./React/scheduled-lessons-list/components/row.css ***!
  \*********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/scheduled-lessons-list-controls.css":
/*!**************************************************************************!*\
  !*** ./React/scheduled-lessons-list/scheduled-lessons-list-controls.css ***!
  \**************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list/scheduled-lessons-list.css":
/*!*****************************************************************!*\
  !*** ./React/scheduled-lessons-list/scheduled-lessons-list.css ***!
  \*****************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/scheduled-lessons-list.ts":
/*!*****************************************!*\
  !*** ./React/scheduled-lessons-list.ts ***!
  \*****************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const scheduled_lessons_list_1 = __importDefault(__webpack_require__(/*! ./scheduled-lessons-list/scheduled-lessons-list */ "./React/scheduled-lessons-list/scheduled-lessons-list.tsx"));
const scheduled_lessons_list_controls_1 = __importDefault(__webpack_require__(/*! ./scheduled-lessons-list/scheduled-lessons-list-controls */ "./React/scheduled-lessons-list/scheduled-lessons-list-controls.tsx"));
globalThis.Components.ScheduledLessonsList = scheduled_lessons_list_1.default;
globalThis.Components.ScheduledLessonsListControls = scheduled_lessons_list_controls_1.default;


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons.tsx":
/*!***********************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons.tsx ***!
  \***********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.LoadNewerLessonsButton = exports.LoadOlderLessonsButton = void 0;
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const layered_button_1 = __webpack_require__(/*! ./load-lessons-buttons/layered-button */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx");
const load_lessons_button_1 = __importDefault(__webpack_require__(/*! ./load-lessons-buttons/load-lessons-button */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.tsx"));
const load_lessons_button_icon_1 = __importDefault(__webpack_require__(/*! ./load-lessons-buttons/load-lessons-button-icon */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.tsx"));
const LoadOlderLessonsButton = (props) => (react_1.default.createElement(load_lessons_button_1.default, { layout: layered_button_1.LoadLessonsButtonLayout.Upright, title: "Wczytaj wcze\u015Bniejsze lekcje", onClick: amount => {
        dispatchEvent(new CustomEvent("loadOlderLessons", {
            detail: { amount }
        }));
    } },
    react_1.default.createElement(load_lessons_button_icon_1.default, { layout: layered_button_1.LoadLessonsButtonLayout.Upright })));
exports.LoadOlderLessonsButton = LoadOlderLessonsButton;
const LoadNewerLessonsButton = (props) => (react_1.default.createElement(load_lessons_button_1.default, { layout: layered_button_1.LoadLessonsButtonLayout.UpsideDown, title: "Wczytaj p\u00F3\u017Aniejsze lekcje", onClick: amount => {
        dispatchEvent(new CustomEvent("loadNewerLessons", {
            detail: { amount }
        }));
    } },
    react_1.default.createElement(load_lessons_button_icon_1.default, { layout: layered_button_1.LoadLessonsButtonLayout.UpsideDown })));
exports.LoadNewerLessonsButton = LoadNewerLessonsButton;


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/animation-events.tsx":
/*!****************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/animation-events.tsx ***!
  \****************************************************************************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
const enum_help_1 = __webpack_require__(/*! ../../../shared/enum-help */ "./React/shared/enum-help.ts");
const layered_button_1 = __webpack_require__(/*! ./layered-button */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx");
class ArrowAnimationEventHelper {
    static dispatch(layout, details) {
        (0, enum_help_1.enumSwitch)(layered_button_1.LoadLessonsButtonLayout, layout, {
            Upright: () => ArrowAnimationEventHelper.dispatchUpright(details),
            UpsideDown: () => ArrowAnimationEventHelper.dispatchUpsideDown(details)
        });
    }
    static dispatchUpright(details) {
        dispatchEvent(createArrowAnimationEvent(layered_button_1.LoadLessonsButtonLayout.Upright, details));
    }
    static dispatchUpsideDown(details) {
        dispatchEvent(createArrowAnimationEvent(layered_button_1.LoadLessonsButtonLayout.UpsideDown, details));
    }
    static addListener(layout, listener) {
        (0, enum_help_1.enumSwitch)(layered_button_1.LoadLessonsButtonLayout, layout, {
            Upright: () => ArrowAnimationEventHelper.addListenerUpright(listener),
            UpsideDown: () => ArrowAnimationEventHelper.addListenerUpsideDown(listener)
        });
    }
    static addListenerUpright(listener) {
        addEventListener(getEventName(layered_button_1.LoadLessonsButtonLayout.Upright), (ev) => {
            listener(ev.detail);
        });
    }
    static addListenerUpsideDown(listener) {
        addEventListener(getEventName(layered_button_1.LoadLessonsButtonLayout.UpsideDown), (ev) => {
            listener(ev.detail);
        });
    }
}
exports["default"] = ArrowAnimationEventHelper;
function createArrowAnimationEvent(layout, detail, init) {
    return new CustomEvent(getEventName(layout), Object.assign({ detail }, init));
}
function getEventName(layout) {
    return (0, enum_help_1.enumAssignSwitch)(layered_button_1.LoadLessonsButtonLayout, layout, {
        Upright: () => "arrow-animation-upright",
        UpsideDown: () => "arrow-animation-upside-down",
        _: () => { throw new Error("Invalid 'LoadLessonsButtonLayout' enum value"); }
    });
}


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx":
/*!**************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx ***!
  \**************************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.LoadLessonsButtonLayout = void 0;
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const enum_switch_1 = __webpack_require__(/*! ../../../shared/enum-help/enum-switch */ "./React/shared/enum-help/enum-switch.ts");
const animation_events_1 = __importDefault(__webpack_require__(/*! ./animation-events */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/animation-events.tsx"));
var LoadLessonsButtonLayout;
(function (LoadLessonsButtonLayout) {
    LoadLessonsButtonLayout[LoadLessonsButtonLayout["Upright"] = 0] = "Upright";
    LoadLessonsButtonLayout[LoadLessonsButtonLayout["UpsideDown"] = 1] = "UpsideDown";
})(LoadLessonsButtonLayout = exports.LoadLessonsButtonLayout || (exports.LoadLessonsButtonLayout = {}));
const classNameUpright = "sll-load-lessons-btn-upright";
const classNameUpsideDown = "sll-load-lessons-btn-upside-down";
const LayeredButton = (props) => {
    return (react_1.default.createElement(ButtonLayer, Object.assign({}, props)));
};
exports["default"] = LayeredButton;
class ButtonLayer extends react_1.default.Component {
    constructor(props) {
        var _a;
        super(props);
        this._index = (_a = this.props.amountIdx) !== null && _a !== void 0 ? _a : 0;
    }
    render() {
        if (!this._postLastLayer) {
            const _a = this.props, { amountIdx } = _a, propsToPass = __rest(_a, ["amountIdx"]);
            const handlers = {
                onClick: this._onClick,
                onMouseEnter: () => this.hover(true),
                onMouseLeave: () => this.hover(false)
            };
            return (react_1.default.createElement("div", Object.assign({ className: this._className, ref: ref => this._buttonRef = ref, role: "button" }, this._lastLayer && !this._postLastLayer
                ? handlers
                : {}),
                react_1.default.createElement("div", { className: "sll-load-lessons-btn-inner" },
                    react_1.default.createElement(ButtonLayer, Object.assign({}, propsToPass, { amountIdx: this._index + 1 }))),
                react_1.default.createElement("div", Object.assign({ className: "sll-load-lessons-btn-right-edge" }, !this._lastLayer && !this._postLastLayer
                    ? handlers
                    : {}),
                    react_1.default.createElement("div", { className: "sll-load-lessons-btn-val" }, this._amount))));
        }
        if (!this.props.children)
            return react_1.default.createElement(react_1.default.Fragment, null);
        return (react_1.default.createElement("div", { className: "sll-load-lessons-inner-content" }, this._childrenWithProps));
    }
    get _lastLayer() { return this._index == this.props.amounts.length - 1; }
    get _postLastLayer() { return this._index >= this.props.amounts.length; }
    get _amount() {
        return !this._postLastLayer
            ? this.props.amounts[this._index]
            : undefined;
    }
    get _className() {
        let className = `sll-load-lessons-btn sll-load-lessons-btn-${this._amount} `;
        className += (0, enum_switch_1.enumAssignSwitch)(LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => classNameUpright,
            UpsideDown: () => classNameUpsideDown,
            _: () => { throw new Error("invalid 'LoadLessonsButtonLayout' enum value"); }
        });
        return className;
    }
    get _onClick() { return () => this.props.onClick(this._amount); }
    hover(hover) {
        if (this._postLastLayer)
            return;
        if (hover)
            this._buttonRef.classList.add("hover");
        else
            this._buttonRef.classList.remove("hover");
        animation_events_1.default.dispatch(this.props.layout, { amount: this._amount, on: hover });
    }
    get _childrenWithProps() {
        var _a;
        (_a = this._childrenWithPropsBackingField) !== null && _a !== void 0 ? _a : (this._childrenWithPropsBackingField = react_1.default.Children
            .map(this.props.children, child => {
            const maxAmount = this.props.amounts.sort((a, b) => a - b).at(-1);
            if (react_1.default.isValidElement(child)) {
                return react_1.default.cloneElement(child, { maxAmount });
            }
            return child;
        }));
        return this._childrenWithPropsBackingField;
    }
}


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.tsx":
/*!************************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.tsx ***!
  \************************************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const enum_help_1 = __webpack_require__(/*! ../../../shared/enum-help */ "./React/shared/enum-help.ts");
const animation_events_1 = __importDefault(__webpack_require__(/*! ./animation-events */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/animation-events.tsx"));
const layered_button_1 = __webpack_require__(/*! ./layered-button */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx");
__webpack_require__(/*! ./load-lessons-button-icon.css */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button-icon.css");
class LoadLessonsButtonIcon extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }
    componentDidMount() {
        animation_events_1.default.addListener(this.props.layout, (details) => {
            var _a;
            if (details.on) {
                this.setState({ animateForAmount: details.amount });
                return;
            }
            if (((_a = this.state) === null || _a === void 0 ? void 0 : _a.animateForAmount) == details.amount) {
                this.setState({ animateForAmount: undefined });
                return;
            }
        });
    }
    render() {
        return (react_1.default.createElement("div", { className: this._boxClassName },
            react_1.default.createElement("div", { className: this._className, style: {
                    animationDuration: `${this._speed}s`,
                } },
                react_1.default.createElement("i", { className: "fa-solid fa-angles-right" }))));
    }
    get _speed() {
        if (!this.state.animateForAmount)
            return 0;
        else {
            return this.props.maxAmount / this.state.animateForAmount * 0.5;
        }
    }
    get _boxClassName() {
        return "all-load-button-icon-box " + (0, enum_help_1.enumAssignSwitch)(layered_button_1.LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => "all-load-button-icon-box-upright",
            UpsideDown: () => "all-load-button-icon-box-upside-down"
        });
    }
    get _className() {
        return "sll-load-button-icon " + (0, enum_help_1.enumAssignSwitch)(layered_button_1.LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => "sll-load-button-icon-upright",
            UpsideDown: () => "sll-load-button-icon-upside-down"
        });
    }
}
exports["default"] = LoadLessonsButtonIcon;


/***/ }),

/***/ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.tsx":
/*!*******************************************************************************************************!*\
  !*** ./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.tsx ***!
  \*******************************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const layered_button_1 = __importDefault(__webpack_require__(/*! ./layered-button */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/layered-button.tsx"));
__webpack_require__(/*! ./load-lessons-button.css */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons/load-lessons-button.css");
class LoadLessonsButton extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: "sll-load-lessons-whole-btn", title: this.props.title },
            react_1.default.createElement(layered_button_1.default, { layout: this.props.layout, amounts: LoadLessonsButton._amounts, onClick: this.props.onClick, children: this.props.children })));
    }
}
exports["default"] = LoadLessonsButton;
LoadLessonsButton._amounts = [5, 10, 20, 50];


/***/ }),

/***/ "./React/scheduled-lessons-list/components/list-body.tsx":
/*!***************************************************************!*\
  !*** ./React/scheduled-lessons-list/components/list-body.tsx ***!
  \***************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const row_1 = __importDefault(__webpack_require__(/*! ./row */ "./React/scheduled-lessons-list/components/row.tsx"));
__webpack_require__(/*! ./list-body.css */ "./React/scheduled-lessons-list/components/list-body.css");
const scheduled_lessons_list_state_1 = __importDefault(__webpack_require__(/*! ../scheduled-lessons-list-state */ "./React/scheduled-lessons-list/scheduled-lessons-list-state.ts"));
class ListBody extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this._laterRenders = false;
    }
    componentDidMount() {
        this._laterRenders = true;
    }
    render() {
        // TODO: map to RowProps on receive from server
        const rows = this.props.rows.map((x, index) => {
            var _a;
            (_a = x.startTime) !== null && _a !== void 0 ? _a : (x.startTime = new Date(x.startTimeTk));
            return (react_1.default.createElement(row_1.default, { key: x.startTimeTk, entryIndex: index, isIncoming: x.startTimeTk == scheduled_lessons_list_state_1.default.incomingAtTk, startTime: x.startTime, duration: x.duration, className: x.className, subjectName: x.subjectName, heldClasses: x.heldClasses, isNew: this._laterRenders && x.alreadyAdded != true }));
        });
        this.props.rows.forEach(entry => entry.alreadyAdded = true);
        return (react_1.default.createElement("tbody", { className: scheduled_lessons_list_state_1.default.tbodyClassName + " scheduled-lessons-list-body" }, rows));
    }
}
exports["default"] = ListBody;


/***/ }),

/***/ "./React/scheduled-lessons-list/components/list-head.tsx":
/*!***************************************************************!*\
  !*** ./React/scheduled-lessons-list/components/list-head.tsx ***!
  \***************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const scheduled_lessons_list_state_1 = __importDefault(__webpack_require__(/*! ../scheduled-lessons-list-state */ "./React/scheduled-lessons-list/scheduled-lessons-list-state.ts"));
__webpack_require__(/*! ./list-head.css */ "./React/scheduled-lessons-list/components/list-head.css");
class ListHead extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("thead", { className: scheduled_lessons_list_state_1.default.theadClassName + " scheduled-lessons-list-head" },
            react_1.default.createElement("tr", { className: scheduled_lessons_list_state_1.default.theadTrClassName },
                react_1.default.createElement("th", null,
                    react_1.default.createElement("span", null, "Czas")),
                react_1.default.createElement("th", null,
                    react_1.default.createElement("span", null, "Klasa")),
                react_1.default.createElement("th", null,
                    react_1.default.createElement("span", null, "Przedmiot")),
                react_1.default.createElement("th", null,
                    react_1.default.createElement("span", null, "Temat zaj\u0119\u0107")),
                react_1.default.createElement("th", null,
                    react_1.default.createElement("span", null, "Frekwencja")),
                react_1.default.createElement("th", null))));
    }
}
exports["default"] = ListHead;


/***/ }),

/***/ "./React/scheduled-lessons-list/components/row-button.tsx":
/*!****************************************************************!*\
  !*** ./React/scheduled-lessons-list/components/row-button.tsx ***!
  \****************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const RowButton = ({ text, className, onClickAsync }) => {
    return (react_1.default.createElement("button", { className: `sll-row-button ${className !== null && className !== void 0 ? className : ''}`, onClick: onClickAsync },
        react_1.default.createElement("span", null, text)));
};
exports["default"] = RowButton;


/***/ }),

/***/ "./React/scheduled-lessons-list/components/row.tsx":
/*!*********************************************************!*\
  !*** ./React/scheduled-lessons-list/components/row.tsx ***!
  \*********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const dates_help_1 = __webpack_require__(/*! ../../shared/dates-help */ "./React/shared/dates-help.ts");
const scheduled_lessons_list_state_1 = __importDefault(__webpack_require__(/*! ../scheduled-lessons-list-state */ "./React/scheduled-lessons-list/scheduled-lessons-list-state.ts"));
const server_1 = __importDefault(__webpack_require__(/*! ../server */ "./React/scheduled-lessons-list/server.ts"));
const row_button_1 = __importDefault(__webpack_require__(/*! ./row-button */ "./React/scheduled-lessons-list/components/row-button.tsx"));
__webpack_require__(/*! ./row.css */ "./React/scheduled-lessons-list/components/row.css");
class Row extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._dateFormat = {
            day: "numeric",
            weekday: 'short',
            month: "short",
            hour: "numeric",
            minute: "2-digit"
        };
        this.openPanelAsync = () => __awaiter(this, void 0, void 0, function* () {
            const params = {
                scheduledTimeTk: (0, dates_help_1.prepareMilisecondsForServer)(this.props.startTime)
            };
            const res = yield server_1.default.getAsync("OpenPanel", params);
            if (res === null || res === void 0 ? void 0 : res.success)
                location.reload();
        });
    }
    componentDidMount() {
        if (this.props.isNew) {
            setTimeout(() => this._rowEl.classList.remove('squeezed'));
        }
    }
    render() {
        return (react_1.default.createElement("tr", Object.assign({ id: (this.props.isIncoming ? "incoming-lesson" : ""), className: scheduled_lessons_list_state_1.default.tbodyTrClassName + " " + (this.props.isNew ? "squeezed" : ""), ref: ref => this._rowEl = ref }, scheduled_lessons_list_state_1.default.entryHeight != undefined
            ? { height: scheduled_lessons_list_state_1.default.entryHeight }
            : {}),
            react_1.default.createElement("td", null,
                react_1.default.createElement("span", null, this.props.startTime.toLocaleString('pl-PL', this._dateFormat))),
            react_1.default.createElement("td", null,
                react_1.default.createElement("span", null, this.props.className)),
            react_1.default.createElement("td", null,
                react_1.default.createElement("span", null, this.props.subjectName)),
            this.props.heldClasses == undefined ? (react_1.default.createElement(react_1.default.Fragment, null,
                react_1.default.createElement("td", null),
                react_1.default.createElement("td", null))) : (react_1.default.createElement(react_1.default.Fragment, null,
                react_1.default.createElement("td", null,
                    react_1.default.createElement("span", null, this.props.heldClasses.topic)),
                react_1.default.createElement("td", null,
                    react_1.default.createElement("span", null,
                        this.props.heldClasses.amountOfPresentStudents,
                        " / ",
                        this.props.heldClasses.amountOfAllStudents)))),
            react_1.default.createElement("td", { className: "sll-entry-button-cell" }, this.renderButton())));
    }
    isSoon() {
        const closeTime = new Date(this.props.startTime.getTime());
        closeTime.setMinutes(closeTime.getMinutes() - scheduled_lessons_list_state_1.default.minutesBeforeLessonIsSoon);
        const now = new Date();
        return closeTime <= now;
    }
    isBeforeEnd() {
        const endTime = new Date(this.props.startTime.getTime());
        endTime.setMinutes(endTime.getMinutes() + this.props.duration);
        const now = new Date();
        return endTime >= now;
    }
    renderButton() {
        const buttonProps = {
            onClickAsync: this.openPanelAsync,
            text: '',
        };
        let closeOrOngoing = this.isSoon() && this.isBeforeEnd();
        if (closeOrOngoing) {
            buttonProps.text = "Poprowadź zajęcia";
            buttonProps.className = "conduct-btn";
        }
        else if (this.props.heldClasses) {
            buttonProps.text = "Szczegóły";
            buttonProps.className = "see-past-details-btn";
        }
        else if (this.props.startTime < new Date()) {
            buttonProps.text = "Uzupełnij";
            buttonProps.className = "see-omitted-btn";
        }
        else {
            buttonProps.text = "Szczegóły nadchodzących";
            buttonProps.className = "see-upcomming-btn";
        }
        return react_1.default.createElement(row_button_1.default, Object.assign({}, buttonProps));
    }
}
exports["default"] = Row;


/***/ }),

/***/ "./React/scheduled-lessons-list/scheduled-lessons-list-controls.tsx":
/*!**************************************************************************!*\
  !*** ./React/scheduled-lessons-list/scheduled-lessons-list-controls.tsx ***!
  \**************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const load_lessons_buttons_1 = __webpack_require__(/*! ./components-controls/load-lessons-buttons */ "./React/scheduled-lessons-list/components-controls/load-lessons-buttons.tsx");
__webpack_require__(/*! ./scheduled-lessons-list-controls.css */ "./React/scheduled-lessons-list/scheduled-lessons-list-controls.css");
class ScheduledLessonsListControls extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: "sll-controls-layout" },
            react_1.default.createElement(load_lessons_buttons_1.LoadOlderLessonsButton, null),
            react_1.default.createElement("div", { className: "sll-filters-panel" }),
            react_1.default.createElement(load_lessons_buttons_1.LoadNewerLessonsButton, null)));
    }
}
exports["default"] = ScheduledLessonsListControls;


/***/ }),

/***/ "./React/scheduled-lessons-list/scheduled-lessons-list-state.ts":
/*!**********************************************************************!*\
  !*** ./React/scheduled-lessons-list/scheduled-lessons-list-state.ts ***!
  \**********************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.assignToState = void 0;
const ScheduledLessonsListState = {
    tableClassName: "",
    theadClassName: "",
    theadTrClassName: "",
    tbodyClassName: "",
    tbodyTrClassName: ""
};
exports["default"] = ScheduledLessonsListState;
function assignToState(values) {
    const props = Object.keys(values);
    for (const prop of props) {
        if (values[prop])
            ScheduledLessonsListState[prop] = values[prop];
    }
}
exports.assignToState = assignToState;


/***/ }),

/***/ "./React/scheduled-lessons-list/scheduled-lessons-list.tsx":
/*!*****************************************************************!*\
  !*** ./React/scheduled-lessons-list/scheduled-lessons-list.tsx ***!
  \*****************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const dates_help_1 = __webpack_require__(/*! ../shared/dates-help */ "./React/shared/dates-help.ts");
const list_body_1 = __importDefault(__webpack_require__(/*! ./components/list-body */ "./React/scheduled-lessons-list/components/list-body.tsx"));
const list_head_1 = __importDefault(__webpack_require__(/*! ./components/list-head */ "./React/scheduled-lessons-list/components/list-head.tsx"));
const scheduled_lessons_list_state_1 = __importStar(__webpack_require__(/*! ./scheduled-lessons-list-state */ "./React/scheduled-lessons-list/scheduled-lessons-list-state.ts"));
__webpack_require__(/*! ./scheduled-lessons-list.css */ "./React/scheduled-lessons-list/scheduled-lessons-list.css");
const server_1 = __importDefault(__webpack_require__(/*! ./server */ "./React/scheduled-lessons-list/server.ts"));
class ScheduledLessonsList extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.fixDatesInEntries(this.props.entries.entries);
        this.state = {
            entries: this.props.entries.entries
        };
        (0, scheduled_lessons_list_state_1.assignToState)(this.props.config);
        if (this.props.entries.incomingAtTk) {
            scheduled_lessons_list_state_1.default.incomingAtTk = (0, dates_help_1.fixMilisecondsFromServer)(this.props.entries.incomingAtTk);
        }
    }
    componentDidMount() {
        addEventListener("loadOlderLessons", (event) => this.loadOlderLessonsAsync(event.detail.amount));
        addEventListener("loadNewerLessons", (event) => this.loadNewerLessonsAsync(event.detail.amount));
    }
    render() {
        return (react_1.default.createElement("table", { className: scheduled_lessons_list_state_1.default.tableClassName + " scheduled-lessons-list" },
            react_1.default.createElement(list_head_1.default, null),
            react_1.default.createElement(list_body_1.default, { rows: this.state.entries })));
    }
    loadOlderLessonsAsync(amount) {
        return __awaiter(this, void 0, void 0, function* () {
            const earliest = this.state.entries.length > 0 ? this.state.entries[0] : null;
            const toTk = earliest != null
                ? (0, dates_help_1.prepareMilisecondsForServer)(earliest.startTimeTk - 1)
                : undefined;
            const req = {
                onlyUpcoming: false,
                toTk,
                limitTo: amount
            };
            const res = yield server_1.default.getAsync("Entries", req);
            if (!res)
                return;
            if (res.incomingAtTk && !scheduled_lessons_list_state_1.default.incomingAtTk)
                scheduled_lessons_list_state_1.default.incomingAtTk = (0, dates_help_1.fixMilisecondsFromServer)(res.incomingAtTk);
            this.fixDatesInEntries(res.entries);
            this.setState({
                entries: [
                    ...res.entries,
                    ...this.state.entries
                ]
            });
        });
    }
    loadNewerLessonsAsync(amount) {
        return __awaiter(this, void 0, void 0, function* () {
            let fromTk;
            if (this.state.entries.length) {
                const latest = this.state.entries[this.state.entries.length - 1];
                const from = new Date(latest.startTimeTk);
                from.setMinutes(from.getMinutes() + latest.duration);
                fromTk = (0, dates_help_1.prepareMilisecondsForServer)(from);
            }
            const req = {
                onlyUpcoming: false,
                fromTk,
                limitTo: amount
            };
            const res = yield server_1.default.getAsync("Entries", req);
            if (!res)
                return;
            if (res.incomingAtTk && !scheduled_lessons_list_state_1.default.incomingAtTk)
                scheduled_lessons_list_state_1.default.incomingAtTk = (0, dates_help_1.fixMilisecondsFromServer)(res.incomingAtTk);
            this.fixDatesInEntries(res.entries);
            this.setState({
                entries: [
                    ...this.state.entries,
                    ...res.entries
                ]
            });
        });
    }
    fixDatesInEntries(entriesToFix) {
        for (const entry of entriesToFix) {
            entry.startTime = (0, dates_help_1.fixDateFromServer)(entry.startTimeTk);
            entry.startTimeTk = entry.startTime.getTime();
        }
    }
}
exports["default"] = ScheduledLessonsList;


/***/ }),

/***/ "./React/scheduled-lessons-list/server.ts":
/*!************************************************!*\
  !*** ./React/scheduled-lessons-list/server.ts ***!
  \************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const server_connection_1 = __importDefault(__webpack_require__(/*! ../shared/server-connection */ "./React/shared/server-connection.tsx"));
const server = new server_connection_1.default("/ConductingClasses/ScheduledLessons");
exports["default"] = server;


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared"], () => (__webpack_exec__("./React/scheduled-lessons-list.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=scheduled_lessons_list.bundle.js.map