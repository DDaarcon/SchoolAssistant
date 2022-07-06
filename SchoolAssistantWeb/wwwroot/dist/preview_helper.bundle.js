"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["preview_helper"],{

/***/ "./React/preview-helper/components/floating-pin.css":
/*!**********************************************************!*\
  !*** ./React/preview-helper/components/floating-pin.css ***!
  \**********************************************************/
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
            react_1.default.createElement("div", { className: `ph-floating-pin ${props.attentionGrabbing ? 'ph-floating-pin-bipping' : ''}`, onClick: props.onClick },
                react_1.default.createElement("span", null, "Menu podgl\u0105du"),
                react_1.default.createElement("span", null, "Dane logowania i wi\u0119cej")))));
};
exports["default"] = FloatingPin;


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
const floating_pin_1 = __importDefault(__webpack_require__(/*! ./components/floating-pin */ "./React/preview-helper/components/floating-pin.tsx"));
__webpack_require__(/*! ./preview-helper.css */ "./React/preview-helper/preview-helper.css");
class PreviewHelper extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.toggleVisibility = () => {
        };
    }
    render() {
        return (react_1.default.createElement("div", { className: "preview-helper ph-shown" },
            react_1.default.createElement(floating_pin_1.default, { onClick: this.toggleVisibility, attentionGrabbing: true })));
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