"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["users_management"],{

/***/ "./React/users-management/users-catalog/controls/users-type-controls.css":
/*!*******************************************************************************!*\
  !*** ./React/users-management/users-catalog/controls/users-type-controls.css ***!
  \*******************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/users-management/users-creation-form/related-object-selector.css":
/*!********************************************************************************!*\
  !*** ./React/users-management/users-creation-form/related-object-selector.css ***!
  \********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/users-management/users-creation-form/related-object-selector/related-object-entry-fields.css":
/*!************************************************************************************************************!*\
  !*** ./React/users-management/users-creation-form/related-object-selector/related-object-entry-fields.css ***!
  \************************************************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/users-management/users-creation-form/user-created-page.css":
/*!**************************************************************************!*\
  !*** ./React/users-management/users-creation-form/user-created-page.css ***!
  \**************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/users-management/users-creation-form/user-type-selector.css":
/*!***************************************************************************!*\
  !*** ./React/users-management/users-creation-form/user-type-selector.css ***!
  \***************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/shared/form-controls/mod-comp-base.tsx":
/*!******************************************************!*\
  !*** ./React/shared/form-controls/mod-comp-base.tsx ***!
  \******************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const validator_1 = __importDefault(__webpack_require__(/*! ../validator */ "./React/shared/validator.ts"));
class ModCompBase extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._validator = new validator_1.default();
        this._validator.forModelGetter(() => this.state.data);
    }
    setStateFn(...modifyMethod) {
        this.setState(prevState => {
            const state = Object.assign({}, prevState);
            modifyMethod
                .forEach(method => method ? method(state) : undefined);
            return state;
        });
    }
    setStateFnData(modifyMethod) {
        this.setStateFn(state => modifyMethod(state.data));
    }
}
exports["default"] = ModCompBase;


/***/ }),

/***/ "./React/users-management.ts":
/*!***********************************!*\
  !*** ./React/users-management.ts ***!
  \***********************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const users_catalog_1 = __importDefault(__webpack_require__(/*! ./users-management/users-catalog */ "./React/users-management/users-catalog.tsx"));
const users_creation_form_1 = __importDefault(__webpack_require__(/*! ./users-management/users-creation-form */ "./React/users-management/users-creation-form.tsx"));
globalThis.Components.UsersCatalog = users_catalog_1.default;
globalThis.Components.UsersCreationForm = users_creation_form_1.default;


/***/ }),

/***/ "./React/users-management/enums/user-type-for-management.ts":
/*!******************************************************************!*\
  !*** ./React/users-management/enums/user-type-for-management.ts ***!
  \******************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
var UserTypeForManagement;
(function (UserTypeForManagement) {
    UserTypeForManagement[UserTypeForManagement["Student"] = 0] = "Student";
    UserTypeForManagement[UserTypeForManagement["Teacher"] = 1] = "Teacher";
    UserTypeForManagement[UserTypeForManagement["Administration"] = 2] = "Administration";
    UserTypeForManagement[UserTypeForManagement["Headmaster"] = 3] = "Headmaster";
    UserTypeForManagement[UserTypeForManagement["SystemAdmin"] = 4] = "SystemAdmin";
    UserTypeForManagement[UserTypeForManagement["Parent"] = 5] = "Parent";
})(UserTypeForManagement || (UserTypeForManagement = {}));
exports["default"] = UserTypeForManagement;


/***/ }),

/***/ "./React/users-management/help/user-type-functions.ts":
/*!************************************************************!*\
  !*** ./React/users-management/help/user-type-functions.ts ***!
  \************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getLabelForUserType = exports.getEnabledUserTypes = void 0;
const enum_help_1 = __webpack_require__(/*! ../../shared/enum-help */ "./React/shared/enum-help.ts");
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const settings_1 = __importDefault(__webpack_require__(/*! ../settings */ "./React/users-management/settings.ts"));
const getEnabledUserTypes = () => {
    const all = (0, enum_help_1.getEnumValues)(user_type_for_management_1.default);
    return all.filter(x => !settings_1.default.DisabledUserTypes.includes(x));
};
exports.getEnabledUserTypes = getEnabledUserTypes;
const getLabelForUserType = (type) => {
    switch (type) {
        case user_type_for_management_1.default.Teacher: return "Nauczyciele";
        case user_type_for_management_1.default.Student: return "Uczniowie";
        case user_type_for_management_1.default.Parent: return "Rodzice";
        default: return "Label missing";
    }
};
exports.getLabelForUserType = getLabelForUserType;


/***/ }),

/***/ "./React/users-management/settings.ts":
/*!********************************************!*\
  !*** ./React/users-management/settings.ts ***!
  \********************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ./enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const SETTINGS = {
    DisabledUserTypes: [
        user_type_for_management_1.default.Administration,
        user_type_for_management_1.default.Headmaster,
        user_type_for_management_1.default.Parent,
        user_type_for_management_1.default.SystemAdmin
    ]
};
exports["default"] = SETTINGS;


/***/ }),

/***/ "./React/users-management/users-catalog.tsx":
/*!**************************************************!*\
  !*** ./React/users-management/users-catalog.tsx ***!
  \**************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ./enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const controls_1 = __importDefault(__webpack_require__(/*! ./users-catalog/controls */ "./React/users-management/users-catalog/controls.tsx"));
const users_list_1 = __importDefault(__webpack_require__(/*! ./users-catalog/users-list */ "./React/users-management/users-catalog/users-list.tsx"));
class UsersCatalog extends react_1.default.Component {
    constructor(props) {
        var _a;
        super(props);
        this.changeUserType = (type) => {
            if (type != this.state.type)
                this.setState({ type });
        };
        this.state = {
            type: (_a = this.props.initialType) !== null && _a !== void 0 ? _a : user_type_for_management_1.default.Teacher
        };
    }
    render() {
        return (react_1.default.createElement("div", { className: "whole-page" },
            react_1.default.createElement(controls_1.default, { changeUsersType: this.changeUserType }),
            react_1.default.createElement(users_list_1.default, { type: this.state.type })));
    }
}
exports["default"] = UsersCatalog;


/***/ }),

/***/ "./React/users-management/users-catalog/controls.tsx":
/*!***********************************************************!*\
  !*** ./React/users-management/users-catalog/controls.tsx ***!
  \***********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const users_type_controls_1 = __importDefault(__webpack_require__(/*! ./controls/users-type-controls */ "./React/users-management/users-catalog/controls/users-type-controls.tsx"));
class Controls extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: "users-list-controls" },
            react_1.default.createElement(users_type_controls_1.default, { changeUsersType: this.props.changeUsersType })));
    }
}
exports["default"] = Controls;


/***/ }),

/***/ "./React/users-management/users-catalog/controls/users-type-controls.tsx":
/*!*******************************************************************************!*\
  !*** ./React/users-management/users-catalog/controls/users-type-controls.tsx ***!
  \*******************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_functions_1 = __webpack_require__(/*! ../../help/user-type-functions */ "./React/users-management/help/user-type-functions.ts");
__webpack_require__(/*! ./users-type-controls.css */ "./React/users-management/users-catalog/controls/users-type-controls.css");
class UsersTypeControls extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._enabledTypes = (0, user_type_functions_1.getEnabledUserTypes)();
    }
    render() {
        return (react_1.default.createElement("div", { className: "users-type-controls" }, this._enabledTypes.map(type => react_1.default.createElement("button", { key: type, className: "user-type-btn tiled-btn", onClick: () => this.props.changeUsersType(type) }, (0, user_type_functions_1.getLabelForUserType)(type)))));
    }
}
exports["default"] = UsersTypeControls;


/***/ }),

/***/ "./React/users-management/users-catalog/server-catalog.ts":
/*!****************************************************************!*\
  !*** ./React/users-management/users-catalog/server-catalog.ts ***!
  \****************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const server_connection_1 = __importDefault(__webpack_require__(/*! ../../shared/server-connection */ "./React/shared/server-connection.tsx"));
const serverCatalog = new server_connection_1.default("/UsersManagement");
exports["default"] = serverCatalog;


/***/ }),

/***/ "./React/users-management/users-catalog/users-list.tsx":
/*!*************************************************************!*\
  !*** ./React/users-management/users-catalog/users-list.tsx ***!
  \*************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const student_users_list_1 = __importDefault(__webpack_require__(/*! ./users-lists/student-users-list */ "./React/users-management/users-catalog/users-lists/student-users-list.tsx"));
const teacher_users_list_1 = __importDefault(__webpack_require__(/*! ./users-lists/teacher-users-list */ "./React/users-management/users-catalog/users-lists/teacher-users-list.tsx"));
class UsersList extends react_1.default.Component {
    render() {
        switch (this.props.type) {
            case user_type_for_management_1.default.Student:
                return react_1.default.createElement(student_users_list_1.default, null);
            case user_type_for_management_1.default.Teacher:
                return react_1.default.createElement(teacher_users_list_1.default, null);
            default:
                return react_1.default.createElement(react_1.default.Fragment, null);
        }
    }
}
exports["default"] = UsersList;


/***/ }),

/***/ "./React/users-management/users-catalog/users-lists/student-users-list.tsx":
/*!*********************************************************************************!*\
  !*** ./React/users-management/users-catalog/users-lists/student-users-list.tsx ***!
  \*********************************************************************************/
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
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const server_catalog_1 = __importDefault(__webpack_require__(/*! ../server-catalog */ "./React/users-management/users-catalog/server-catalog.ts"));
const users_list_base_1 = __importDefault(__webpack_require__(/*! ./users-list-base */ "./React/users-management/users-catalog/users-lists/users-list-base.tsx"));
class StudentUsersList extends users_list_base_1.default {
    constructor() {
        super(...arguments);
        this.loadAsync = () => __awaiter(this, void 0, void 0, function* () {
            const params = {
                ofType: user_type_for_management_1.default.Student
            };
            let response = yield server_catalog_1.default.getAsync("UserListEntries", params);
            return response;
        });
    }
    extraColumnsSettings() {
        return [
            {
                header: "Klasa",
                prop: "orgClass"
            }
        ];
    }
    render() {
        return super.render();
    }
}
exports["default"] = StudentUsersList;


/***/ }),

/***/ "./React/users-management/users-catalog/users-lists/teacher-users-list.tsx":
/*!*********************************************************************************!*\
  !*** ./React/users-management/users-catalog/users-lists/teacher-users-list.tsx ***!
  \*********************************************************************************/
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
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const server_catalog_1 = __importDefault(__webpack_require__(/*! ../server-catalog */ "./React/users-management/users-catalog/server-catalog.ts"));
const users_list_base_1 = __importDefault(__webpack_require__(/*! ./users-list-base */ "./React/users-management/users-catalog/users-lists/users-list-base.tsx"));
class TeacherUsersList extends users_list_base_1.default {
    constructor() {
        super(...arguments);
        this.loadAsync = () => __awaiter(this, void 0, void 0, function* () {
            const params = {
                ofType: user_type_for_management_1.default.Teacher
            };
            let response = yield server_catalog_1.default.getAsync("UserListEntries", params);
            return response;
        });
    }
    render() {
        return super.render();
    }
}
exports["default"] = TeacherUsersList;


/***/ }),

/***/ "./React/users-management/users-catalog/users-lists/users-list-base.tsx":
/*!******************************************************************************!*\
  !*** ./React/users-management/users-catalog/users-lists/users-list-base.tsx ***!
  \******************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const lists_1 = __webpack_require__(/*! ../../../shared/lists */ "./React/shared/lists.ts");
class UsersListBase extends react_1.default.Component {
    constructor(props) {
        var _a, _b;
        super(props);
        this._columnsSetting = [
            {
                header: "Nazwisko",
                prop: "lastName",
            },
            {
                header: "Imię",
                prop: "firstName"
            },
            {
                header: "Nazwa użytkownika",
                prop: "userName"
            },
            {
                header: "Email",
                prop: "email"
            },
            ...((_b = (_a = this.extraColumnsSettings) === null || _a === void 0 ? void 0 : _a.call(this)) !== null && _b !== void 0 ? _b : [])
        ];
    }
    render() {
        return (react_1.default.createElement(lists_1.ReadonlyList, { columnsSetting: this._columnsSetting, loadDataAsync: this.loadAsync }));
    }
}
exports["default"] = UsersListBase;


/***/ }),

/***/ "./React/users-management/users-creation-form.tsx":
/*!********************************************************!*\
  !*** ./React/users-management/users-creation-form.tsx ***!
  \********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const enum_help_1 = __webpack_require__(/*! ../shared/enum-help */ "./React/shared/enum-help.ts");
const top_bar_1 = __importDefault(__webpack_require__(/*! ../shared/top-bar */ "./React/shared/top-bar.tsx"));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ./enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const related_object_selector_1 = __importDefault(__webpack_require__(/*! ./users-creation-form/related-object-selector */ "./React/users-management/users-creation-form/related-object-selector.tsx"));
const user_created_page_1 = __importDefault(__webpack_require__(/*! ./users-creation-form/user-created-page */ "./React/users-management/users-creation-form/user-created-page.tsx"));
const user_details_form_1 = __importDefault(__webpack_require__(/*! ./users-creation-form/user-details-form */ "./React/users-management/users-creation-form/user-details-form.tsx"));
const user_type_selector_1 = __importDefault(__webpack_require__(/*! ./users-creation-form/user-type-selector */ "./React/users-management/users-creation-form/user-type-selector.tsx"));
class UsersCreationForm extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.selectType = (type) => {
            if (!(0, enum_help_1.isValidEnumValue)(user_type_for_management_1.default, type))
                return;
            this.setState({ selectedType: type, selectedObject: undefined });
        };
        this.selectRelatedObject = (obj) => this.setState({ selectedObject: obj });
        this.redirectFromEdition = (createdUser) => this.setState({ selectedObject: undefined, createdUser });
        this.returnToSelector = () => this.setState({ createdUser: undefined });
    }
    render() {
        return (react_1.default.createElement("div", { className: "whole-page" },
            react_1.default.createElement(top_bar_1.default, null),
            this.renderContent()));
    }
    renderContent() {
        var _a;
        if (((_a = this.state) === null || _a === void 0 ? void 0 : _a.selectedType) == undefined)
            return (react_1.default.createElement(user_type_selector_1.default, { selectType: this.selectType }));
        if (this.state.selectedObject == undefined && this.state.createdUser == undefined)
            return (react_1.default.createElement(related_object_selector_1.default, { type: this.state.selectedType, selectRelatedObject: this.selectRelatedObject, backToTypeSelect: () => this.setState({ selectedType: undefined }) }));
        if (this.state.createdUser == undefined)
            return (react_1.default.createElement(user_details_form_1.default, { type: this.state.selectedType, object: this.state.selectedObject, changePage: this.redirectFromEdition, backToObjectSelect: () => this.setState({ selectedObject: undefined }) }));
        return (react_1.default.createElement(user_created_page_1.default, { user: this.state.createdUser, returnToSelector: this.returnToSelector }));
    }
}
exports["default"] = UsersCreationForm;


/***/ }),

/***/ "./React/users-management/users-creation-form/related-object-selector.tsx":
/*!********************************************************************************!*\
  !*** ./React/users-management/users-creation-form/related-object-selector.tsx ***!
  \********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const related_objects_lists_spec_1 = __webpack_require__(/*! ./related-object-selector/related-objects-lists-spec */ "./React/users-management/users-creation-form/related-object-selector/related-objects-lists-spec.tsx");
__webpack_require__(/*! ./related-object-selector.css */ "./React/users-management/users-creation-form/related-object-selector.css");
const top_bar_1 = __importDefault(__webpack_require__(/*! ../../shared/top-bar */ "./React/shared/top-bar.tsx"));
class RelatedObjectSelector extends react_1.default.Component {
    render() {
        top_bar_1.default.Ref.setGoBackAction(this.props.backToTypeSelect);
        return (react_1.default.createElement("div", { className: "related-object-selector" }, this.renderObjectsList()));
    }
    renderObjectsList() {
        switch (this.props.type) {
            case user_type_for_management_1.default.Student:
                return (react_1.default.createElement(related_objects_lists_spec_1.StudentObjectsList, { selectObject: this.props.selectRelatedObject }));
            case user_type_for_management_1.default.Teacher:
                return (react_1.default.createElement(related_objects_lists_spec_1.TeacherObjectsList, { selectObject: this.props.selectRelatedObject }));
            case user_type_for_management_1.default.Parent:
                return (react_1.default.createElement(related_objects_lists_spec_1.ParentObjectsList, { selectObject: this.props.selectRelatedObject }));
            default: throw new Error("Not implemented yet");
        }
    }
}
exports["default"] = RelatedObjectSelector;


/***/ }),

/***/ "./React/users-management/users-creation-form/related-object-selector/related-objects-list.tsx":
/*!*****************************************************************************************************!*\
  !*** ./React/users-management/users-creation-form/related-object-selector/related-objects-list.tsx ***!
  \*****************************************************************************************************/
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
const server_creation_form_1 = __importDefault(__webpack_require__(/*! ../server-creation-form */ "./React/users-management/users-creation-form/server-creation-form.ts"));
class RelatedObjectsList extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.objectDisplayComponent = (obj) => {
            const fields = this.props.objectToFields(obj);
            const elements = [];
            for (let i = 0; i < Math.min(fields.length, this.props.fieldClassNames.length); i++)
                elements.push(react_1.default.createElement("div", { key: i, className: `related-object-entry-field ${this.props.fieldClassNames[i]}` }, fields[i]));
            return (react_1.default.createElement("button", { key: `${this.props.type}-${obj.id}`, className: "related-object-entry tiled-btn", onClick: () => this.props.selectObject(obj) }, elements));
        };
        this.fetchObjectsAsync();
    }
    render() {
        var _a, _b;
        return (react_1.default.createElement(react_1.default.Fragment, null, (_b = (_a = this.state) === null || _a === void 0 ? void 0 : _a.objects) === null || _b === void 0 ? void 0 : _b.map(this.objectDisplayComponent)));
    }
    fetchObjectsAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            const params = {
                ofType: this.props.type
            };
            const res = yield server_creation_form_1.default.getAsync('RelatedObjects', params);
            this.setState({ objects: res });
        });
    }
}
exports["default"] = RelatedObjectsList;


/***/ }),

/***/ "./React/users-management/users-creation-form/related-object-selector/related-objects-lists-spec.tsx":
/*!***********************************************************************************************************!*\
  !*** ./React/users-management/users-creation-form/related-object-selector/related-objects-lists-spec.tsx ***!
  \***********************************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.ParentObjectsList = exports.TeacherObjectsList = exports.StudentObjectsList = void 0;
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_for_management_1 = __importDefault(__webpack_require__(/*! ../../enums/user-type-for-management */ "./React/users-management/enums/user-type-for-management.ts"));
const related_objects_list_1 = __importDefault(__webpack_require__(/*! ./related-objects-list */ "./React/users-management/users-creation-form/related-object-selector/related-objects-list.tsx"));
__webpack_require__(/*! ./related-object-entry-fields.css */ "./React/users-management/users-creation-form/related-object-selector/related-object-entry-fields.css");
const StudentObjectsList = (props) => (react_1.default.createElement(related_objects_list_1.default, { objectToFields: obj => ([obj.lastName, obj.firstName, obj.email, obj.orgClass]), fieldClassNames: ['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email', 'stu-obj-ent-org-class'], selectObject: props.selectObject, type: user_type_for_management_1.default.Student }));
exports.StudentObjectsList = StudentObjectsList;
const TeacherObjectsList = (props) => (react_1.default.createElement(related_objects_list_1.default, { objectToFields: obj => ([obj.lastName, obj.firstName, obj.email]), fieldClassNames: ['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email'], selectObject: props.selectObject, type: user_type_for_management_1.default.Teacher }));
exports.TeacherObjectsList = TeacherObjectsList;
const ParentObjectsList = (props) => (react_1.default.createElement(related_objects_list_1.default, { objectToFields: obj => ([obj.lastName, obj.firstName, obj.email]), fieldClassNames: ['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email'], selectObject: props.selectObject, type: user_type_for_management_1.default.Parent }));
exports.ParentObjectsList = ParentObjectsList;


/***/ }),

/***/ "./React/users-management/users-creation-form/server-creation-form.ts":
/*!****************************************************************************!*\
  !*** ./React/users-management/users-creation-form/server-creation-form.ts ***!
  \****************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const server_connection_1 = __importDefault(__webpack_require__(/*! ../../shared/server-connection */ "./React/shared/server-connection.tsx"));
const serverCreationForm = new server_connection_1.default('/UsersManagement/CreateUser');
exports["default"] = serverCreationForm;


/***/ }),

/***/ "./React/users-management/users-creation-form/user-created-page.tsx":
/*!**************************************************************************!*\
  !*** ./React/users-management/users-creation-form/user-created-page.tsx ***!
  \**************************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const top_bar_1 = __importDefault(__webpack_require__(/*! ../../shared/top-bar */ "./React/shared/top-bar.tsx"));
const server_creation_form_1 = __importDefault(__webpack_require__(/*! ./server-creation-form */ "./React/users-management/users-creation-form/server-creation-form.ts"));
__webpack_require__(/*! ./user-created-page.css */ "./React/users-management/users-creation-form/user-created-page.css");
class UserCreatedPage extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.passwordInfoComponent = () => {
            if (this.state.readablePassword == undefined)
                return (react_1.default.createElement("div", null,
                    "(",
                    react_1.default.createElement("a", { href: "#", onClick: this.unscramblePasswordAsync }, "Poka\u017C"),
                    ")",
                    react_1.default.createElement("span", null, this.printStars())));
            return (react_1.default.createElement("span", null, this.state.readablePassword));
        };
        this.unscramblePasswordAsync = () => __awaiter(this, void 0, void 0, function* () {
            var res = yield server_creation_form_1.default.getAsync("UnscramblePassword", {
                deformed: this.props.user.passwordDeformed
            });
            this.setState({ readablePassword: res.readablePassword });
        });
        this.state = {};
    }
    render() {
        top_bar_1.default.Ref.setGoBackAction(this.props.returnToSelector);
        return (react_1.default.createElement("div", { className: "user-created-page" },
            react_1.default.createElement("h2", null, "Utworzono u\u017Cytkownika"),
            react_1.default.createElement("div", { className: "user-created-page-rows" },
                react_1.default.createElement("div", { className: "usr-crea-page-user-info-row" },
                    react_1.default.createElement(form_controls_1.LabelValue, { label: "Nazwisko i imi\u0119", valueComp: react_1.default.createElement("span", null, `${this.props.user.lastName} ${this.props.user.firstName}`) }),
                    react_1.default.createElement(form_controls_1.LabelValue, { label: "Nazwa u\u017Cytkownika", valueComp: react_1.default.createElement("span", null, this.props.user.userName) }),
                    react_1.default.createElement(form_controls_1.LabelValue, { label: "Email", valueComp: react_1.default.createElement("span", null, this.props.user.email) }),
                    react_1.default.createElement(form_controls_1.LabelValue, { label: "Has\u0142o", valueComp: react_1.default.createElement("span", null, this.passwordInfoComponent()) })),
                react_1.default.createElement("div", { className: "usr-crea-page-messages-row" },
                    react_1.default.createElement("p", null, "Uzytkownik zosta\u0142 utworzony. Na adres podany adres email zosta\u0142o wys\u0142ane has\u0142o tymczasowe has\u0142o. Po zalogowaniu si\u0119 u\u017Cytkownik powinnien je zmieni\u0107.")))));
    }
    printStars() {
        let count = this.props.user.passwordDeformed.length;
        let stars = "";
        while (0 < count--)
            stars += "*";
        return stars;
    }
}
exports["default"] = UserCreatedPage;


/***/ }),

/***/ "./React/users-management/users-creation-form/user-details-form.tsx":
/*!**************************************************************************!*\
  !*** ./React/users-management/users-creation-form/user-details-form.tsx ***!
  \**************************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const top_bar_1 = __importDefault(__webpack_require__(/*! ../../shared/top-bar */ "./React/shared/top-bar.tsx"));
const server_creation_form_1 = __importDefault(__webpack_require__(/*! ./server-creation-form */ "./React/users-management/users-creation-form/server-creation-form.ts"));
class UserDetailsForm extends mod_comp_base_1.default {
    constructor(props) {
        var _a;
        super(props);
        this.renderRelatedObjectInfo = () => {
            const info = Object.keys(this.props.object).map(key => key == 'id' || key == 'email' || key == 'firstName' || key == 'lastName' ? '' : this.props.object[key])
                .filter(x => x);
            return (react_1.default.createElement("div", { className: "related-object-info" },
                react_1.default.createElement("p", null, "Tworzysz u\u017Cytkonika dla osoby:"),
                react_1.default.createElement("h3", null,
                    this.props.object.lastName,
                    " ",
                    this.props.object.firstName),
                react_1.default.createElement("p", null, info.join(', '))));
        };
        this.changeUserName = (ev) => {
            const value = ev.target.value;
            this.setStateFnData(data => data.userName = value);
        };
        this.changeEmail = (ev) => {
            const value = ev.target.value;
            this.setStateFnData(data => data.email = value);
        };
        this.changePhoneNumber = (ev) => {
            const value = ev.target.value;
            this.setStateFnData(data => data.phoneNumber = value);
        };
        this.submitAsync = (e) => __awaiter(this, void 0, void 0, function* () {
            e.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            var res = yield server_creation_form_1.default.postAsync("AddUser", undefined, this.state.data);
            if (res.success) {
                this.props.changePage({
                    firstName: this.props.object.firstName,
                    lastName: this.props.object.lastName,
                    userName: this.state.data.userName,
                    email: this.state.data.email,
                    passwordDeformed: res.passwordDeformed
                });
            }
            else
                console.debug(res.message);
        });
        this.state = {
            data: {
                userName: '',
                email: (_a = this.props.object.email) !== null && _a !== void 0 ? _a : '',
                relatedType: this.props.type,
                relatedId: this.props.object.id
            }
        };
        this._validator.setRules({
            userName: {
                notNull: "Należy podać nazwę użytkownika",
                notEmpty: "Należy podać nazwę użytkownika",
                other: (model, prop) => {
                    // TODO: Check if userName is taken
                    return undefined;
                }
            },
            email: {
                notNull: "Należy podać adres email",
                notEmpty: "Należy podać adres email",
                other: (model, prop) => {
                    // TODO: Check if email has a valid form
                    return undefined;
                }
            }
        });
    }
    render() {
        top_bar_1.default.Ref.setGoBackAction(this.props.backToObjectSelect);
        return (react_1.default.createElement("form", { onSubmit: this.submitAsync },
            this.renderRelatedObjectInfo(),
            react_1.default.createElement(form_controls_1.Input, { label: "Nazwa u\u017Cytkownika", name: "username-input", value: this.state.data.userName, onChange: this.changeUserName, errorMessages: this._validator.getErrorMsgsFor('userName'), type: "text" }),
            react_1.default.createElement(form_controls_1.Input, { label: "Adres email", name: "email-input", value: this.state.data.email, onChange: this.changeEmail, errorMessages: this._validator.getErrorMsgsFor('email'), type: "email" }),
            react_1.default.createElement(form_controls_1.Input, { label: "Numer telefonu", name: "phone-number-input", value: this.state.data.phoneNumber, onChange: this.changePhoneNumber, errorMessages: this._validator.getErrorMsgsFor('phoneNumber'), type: "tel" }),
            react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" })));
    }
}
exports["default"] = UserDetailsForm;


/***/ }),

/***/ "./React/users-management/users-creation-form/user-type-selector.tsx":
/*!***************************************************************************!*\
  !*** ./React/users-management/users-creation-form/user-type-selector.tsx ***!
  \***************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const user_type_functions_1 = __webpack_require__(/*! ../help/user-type-functions */ "./React/users-management/help/user-type-functions.ts");
__webpack_require__(/*! ./user-type-selector.css */ "./React/users-management/users-creation-form/user-type-selector.css");
class UserTypeSelector extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._enabledTypes = (0, user_type_functions_1.getEnabledUserTypes)();
    }
    render() {
        return (react_1.default.createElement("div", { className: "user-type-selector" }, this._enabledTypes.map(type => react_1.default.createElement("button", { key: type, className: "user-type-btn tiled-btn", onClick: () => this.props.selectType(type) }, (0, user_type_functions_1.getLabelForUserType)(type)))));
    }
}
exports["default"] = UserTypeSelector;


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared"], () => (__webpack_exec__("./React/users-management.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=users_management.bundle.js.map