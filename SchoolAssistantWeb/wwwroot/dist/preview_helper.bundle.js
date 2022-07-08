"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["preview_helper"],{

/***/ "./React/preview-helper/components/default-menu.css":
/*!**********************************************************!*\
  !*** ./React/preview-helper/components/default-menu.css ***!
  \**********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/preview-helper/components/floating-pin.css":
/*!**********************************************************!*\
  !*** ./React/preview-helper/components/floating-pin.css ***!
  \**********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/preview-helper/components/login-menu.css":
/*!********************************************************!*\
  !*** ./React/preview-helper/components/login-menu.css ***!
  \********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/preview-helper/preview-helper.css":
/*!*************************************************!*\
  !*** ./React/preview-helper/preview-helper.css ***!
  \*************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/preview-helper.ts":
/*!*********************************!*\
  !*** ./React/preview-helper.ts ***!
  \*********************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const preview_helper_1 = __importDefault(__webpack_require__(/*! ./preview-helper/preview-helper */ "./React/preview-helper/preview-helper.tsx"));
globalThis.Components.PreviewHelper = preview_helper_1.default;


/***/ }),

/***/ "./React/preview-helper/components/default-menu.tsx":
/*!**********************************************************!*\
  !*** ./React/preview-helper/components/default-menu.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const components_1 = __webpack_require__(/*! ../../shared/components */ "./React/shared/components.ts");
__webpack_require__(/*! ./default-menu.css */ "./React/preview-helper/components/default-menu.css");
const DefaultMenu = (props) => {
    const resetAppData = () => {
        console.log("reset data");
    };
    const createLessonNow = () => {
    };
    return (react_1.default.createElement("div", { className: "ph-default-menu" },
        react_1.default.createElement(components_1.ActionButton, { label: "Zresetuj dane aplikacji", className: "ph-default-menu-button", onClick: resetAppData }),
        react_1.default.createElement("p", null, "Zresetuj przedmioty, nauczycieli, pomieszczenia, klasy, uczni\u00F3w, oceny oraz zaj\u0119cia do stanu stanu pocz\u0105tkowego."),
        react_1.default.createElement(components_1.ActionButton, { label: "Utw\u00F3rz zaj\u0119cia na teraz", className: "ph-default-menu-button", onClick: createLessonNow }),
        react_1.default.createElement("p", null, "Utw\u00F3rz zaj\u0119cia dla przyk\u0142adowego nauczyciela. Aby je poprowadzi\u0107 zaloguj si\u0119 na to konto i wybierz 'Poprowad\u017A zaj\u0119cia' z listy zaj\u0119\u0107.")));
};
exports["default"] = DefaultMenu;


/***/ }),

/***/ "./React/preview-helper/components/floating-pin.tsx":
/*!**********************************************************!*\
  !*** ./React/preview-helper/components/floating-pin.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
__webpack_require__(/*! ./floating-pin.css */ "./React/preview-helper/components/floating-pin.css");
const FloatingPin = (props) => {
    return (react_1.default.createElement("div", { className: "ph-floating-pin-container" },
        react_1.default.createElement("div", { className: "ph-floating-pin-container-in" },
            react_1.default.createElement("div", { className: `ph-floating-pin ${props.attentionGrabbing ? 'ph-floating-pin-bipping ph-floating-pin-menu-hidden' : ''}`, onClick: props.onClick },
                react_1.default.createElement("span", null, "Menu podgl\u0105du"),
                react_1.default.createElement("span", null, props.textOnHover)))));
};
exports["default"] = FloatingPin;


/***/ }),

/***/ "./React/preview-helper/components/login-menu.tsx":
/*!********************************************************!*\
  !*** ./React/preview-helper/components/login-menu.tsx ***!
  \********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
__webpack_require__(/*! ./login-menu.css */ "./React/preview-helper/components/login-menu.css");
const LoginMenu = (props) => {
    const DisplayCredentials = ({ header, userName, password }) => {
        if (!userName || !password)
            return react_1.default.createElement(react_1.default.Fragment, null);
        return (react_1.default.createElement(react_1.default.Fragment, null,
            react_1.default.createElement("p", { className: "ph-login-menu-header" }, header),
            react_1.default.createElement(form_controls_1.LabelValue, { label: "Nazwa u\u017Cytkownika", value: userName, width: 260 }),
            react_1.default.createElement(form_controls_1.LabelValue, { label: "Has\u0142o", value: password, width: 260 })));
    };
    return (react_1.default.createElement("div", { className: "ph-login-menu" },
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania administratora", userName: props.logins.administratorUserName, password: props.logins.administratorPassword }),
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania przyk\u0142adowego nauczyciela", userName: props.logins.teacherUserName, password: props.logins.teacherPassword }),
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania przyk\u0142adowego ucznia", userName: props.logins.studentUserName, password: props.logins.studentPassword })));
};
exports["default"] = LoginMenu;


/***/ }),

/***/ "./React/preview-helper/enums/preview-menu-type.ts":
/*!*********************************************************!*\
  !*** ./React/preview-helper/enums/preview-menu-type.ts ***!
  \*********************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
var PreviewMenuType;
(function (PreviewMenuType) {
    PreviewMenuType[PreviewMenuType["LoginMenu"] = 0] = "LoginMenu";
})(PreviewMenuType || (PreviewMenuType = {}));
exports["default"] = PreviewMenuType;


/***/ }),

/***/ "./React/preview-helper/preview-helper.tsx":
/*!*************************************************!*\
  !*** ./React/preview-helper/preview-helper.tsx ***!
  \*************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const enum_help_1 = __webpack_require__(/*! ../shared/enum-help */ "./React/shared/enum-help.ts");
const default_menu_1 = __importDefault(__webpack_require__(/*! ./components/default-menu */ "./React/preview-helper/components/default-menu.tsx"));
const floating_pin_1 = __importDefault(__webpack_require__(/*! ./components/floating-pin */ "./React/preview-helper/components/floating-pin.tsx"));
const login_menu_1 = __importDefault(__webpack_require__(/*! ./components/login-menu */ "./React/preview-helper/components/login-menu.tsx"));
const preview_menu_type_1 = __importDefault(__webpack_require__(/*! ./enums/preview-menu-type */ "./React/preview-helper/enums/preview-menu-type.ts"));
__webpack_require__(/*! ./preview-helper.css */ "./React/preview-helper/preview-helper.css");
class PreviewHelper extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.toggleVisibility = () => {
            this.setState({ hidden: !this.state.hidden });
        };
        this.state = {
            hidden: true
        };
    }
    render() {
        return (react_1.default.createElement("div", { className: `preview-helper ${this.state.hidden ? 'ph-hidden' : ''}` },
            react_1.default.createElement(floating_pin_1.default, { textOnHover: this.getTextOnHoverForPin(), onClick: this.toggleVisibility, attentionGrabbing: this.state.hidden }),
            this.renderMenu()));
    }
    renderMenu() {
        var _a;
        return (0, enum_help_1.enumAssignSwitch)(preview_menu_type_1.default, (_a = this.props) === null || _a === void 0 ? void 0 : _a.type, {
            LoginMenu: () => this.props.logins != undefined
                ? react_1.default.createElement(login_menu_1.default, { logins: this.props.logins })
                : react_1.default.createElement(default_menu_1.default, null),
            _: react_1.default.createElement(default_menu_1.default, null)
        });
    }
    getTextOnHoverForPin() {
        var _a;
        return (0, enum_help_1.enumAssignSwitch)(preview_menu_type_1.default, (_a = this.props) === null || _a === void 0 ? void 0 : _a.type, {
            LoginMenu: "Dane logowania",
            _: "Ustaw podglądowe dane"
        });
    }
}
exports["default"] = PreviewHelper;


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared"], () => (__webpack_exec__("./React/preview-helper.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=preview_helper.bundle.js.map