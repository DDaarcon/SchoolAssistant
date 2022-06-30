"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["data_management"],{

/***/ "./React/data-management/main.css":
/*!****************************************!*\
  !*** ./React/data-management/main.css ***!
  \****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/data-management/navigation.css":
/*!**********************************************!*\
  !*** ./React/data-management/navigation.css ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/data-management/students/students.css":
/*!*****************************************************!*\
  !*** ./React/data-management/students/students.css ***!
  \*****************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/data-management.ts":
/*!**********************************!*\
  !*** ./React/data-management.ts ***!
  \**********************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const main_1 = __importDefault(__webpack_require__(/*! ./data-management/main */ "./React/data-management/main.tsx"));
globalThis.Components.DataManagement = main_1.default;


/***/ }),

/***/ "./React/data-management/classes/class-list.tsx":
/*!******************************************************!*\
  !*** ./React/data-management/classes/class-list.tsx ***!
  \******************************************************/
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
const lists_1 = __webpack_require__(/*! ../../shared/lists */ "./React/shared/lists.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const class_mod_comp_1 = __importDefault(__webpack_require__(/*! ./class-mod-comp */ "./React/data-management/classes/class-mod-comp.tsx"));
const ClassesList = (props) => {
    const columnsSetting = [
        {
            header: "Klasa",
            prop: "name",
        },
        {
            header: "Kierunek",
            prop: "specialization"
        },
        {
            header: "Liczba uczniów",
            prop: "amountOfStudents"
        }
    ];
    const loadAsync = () => __awaiter(void 0, void 0, void 0, function* () {
        let response = yield main_1.server.getAsync("ClassEntries");
        return response;
    });
    return (react_1.default.createElement(lists_1.List, { columnsSetting: columnsSetting, loadDataAsync: loadAsync, modificationComponent: class_mod_comp_1.default, customButtons: [{
                label: "Uczniowie",
                action: (entry) => props.onMoveToStudents({
                    classId: entry.id,
                    className: entry.name,
                    classSpecialization: entry.specialization
                }),
                columnStyle: {
                    width: '1%',
                    minWidth: 150
                }
            }] }));
};
exports["default"] = ClassesList;


/***/ }),

/***/ "./React/data-management/classes/class-mod-comp.tsx":
/*!**********************************************************!*\
  !*** ./React/data-management/classes/class-mod-comp.tsx ***!
  \**********************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const loader_1 = __importStar(__webpack_require__(/*! ../../shared/loader */ "./React/shared/loader.tsx"));
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
class ClassModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.createOnTextChangeHandler = (property) => {
            return (event) => {
                const value = event.target.value;
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    data[property] = value;
                    return { data };
                });
                this.props.onMadeAnyChange();
            };
        };
        this.onSubmitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("ClassData", undefined, Object.assign({}, this.state.data));
            if (response.success)
                yield this.props.reloadAsync();
            else
                console.debug(response);
        });
        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                grade: 1,
                distinction: '',
                specialization: ''
            }
        };
        this._validator.setRules({
            grade: {
                notNull: true, other: (model, prop) => {
                    if (model.grade < 0)
                        return {
                            error: "Wartość poniżej 0 jest nieprawidłowa",
                            on: prop
                        };
                }
            },
            distinction: {
                other: (model, prop) => {
                    var _a;
                    if (((_a = model.distinction) === null || _a === void 0 ? void 0 : _a.length) > 4)
                        return {
                            error: "Rozróżnienie klasy powinno być krótsze",
                            on: prop
                        };
                }
            }
        });
        if (this.props.recordId)
            this.fetchAsync();
    }
    render() {
        if (this.state.awaitingData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.onSubmitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "grade-input", label: "Numer klasy", value: this.state.data.grade, onChange: this.createOnTextChangeHandler('grade'), errorMessages: this._validator.getErrorMsgsFor('grade'), type: "number" }),
                react_1.default.createElement(form_controls_1.Input, { name: "distinction-input", label: "Dodatkowe rozr\u00F3\u017Cnienie", value: this.state.data.distinction, onChange: this.createOnTextChangeHandler('distinction'), errorMessages: this._validator.getErrorMsgsFor('distinction'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "specialization-input", label: "Kierunek", value: this.state.data.specialization, onChange: this.createOnTextChangeHandler('specialization'), errorMessages: this._validator.getErrorMsgsFor('specialization'), type: "text" }),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let response = yield main_1.server.getAsync("ClassModificationData", {
                id: this.props.recordId
            });
            this.setState({ data: response.data, awaitingData: false });
        });
    }
}
exports["default"] = ClassModComp;


/***/ }),

/***/ "./React/data-management/classes/classes-page.tsx":
/*!********************************************************!*\
  !*** ./React/data-management/classes/classes-page.tsx ***!
  \********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const category_1 = __importDefault(__webpack_require__(/*! ../enums/category */ "./React/data-management/enums/category.ts"));
const students_page_1 = __importDefault(__webpack_require__(/*! ../students/students-page */ "./React/data-management/students/students-page.tsx"));
const class_list_1 = __importDefault(__webpack_require__(/*! ./class-list */ "./React/data-management/classes/class-list.tsx"));
class ClassesPage extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.moveToStudents = (studentsPageProps) => {
            this.props.onRedirect(category_1.default.Students, students_page_1.default, studentsPageProps);
        };
    }
    render() {
        return (react_1.default.createElement(class_list_1.default, { onMoveToStudents: this.moveToStudents }));
    }
}
exports["default"] = ClassesPage;


/***/ }),

/***/ "./React/data-management/enums/category.ts":
/*!*************************************************!*\
  !*** ./React/data-management/enums/category.ts ***!
  \*************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
var Category;
(function (Category) {
    Category[Category["Subjects"] = 0] = "Subjects";
    Category[Category["Staff"] = 1] = "Staff";
    Category[Category["Rooms"] = 2] = "Rooms";
    Category[Category["Classes"] = 3] = "Classes";
    Category[Category["Students"] = 4] = "Students";
})(Category || (Category = {}));
exports["default"] = Category;


/***/ }),

/***/ "./React/data-management/main.tsx":
/*!****************************************!*\
  !*** ./React/data-management/main.tsx ***!
  \****************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.server = void 0;
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const modals_1 = __webpack_require__(/*! ../shared/modals */ "./React/shared/modals.ts");
const server_connection_1 = __importDefault(__webpack_require__(/*! ../shared/server-connection */ "./React/shared/server-connection.tsx"));
const navigation_1 = __importDefault(__webpack_require__(/*! ./navigation */ "./React/data-management/navigation.tsx"));
__webpack_require__(/*! ./main.css */ "./React/data-management/main.css");
exports.server = new server_connection_1.default("/DataManagement");
class DataManagementMainScreen extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.state = {
            active: undefined,
            pageComponent: undefined
        };
        this.redirect = (type, pageComponent, props) => {
            this.setState({
                active: type,
                pageComponent: pageComponent,
                props: props
            });
        };
    }
    renderPageContent() {
        var _a;
        if ((_a = this.state) === null || _a === void 0 ? void 0 : _a.pageComponent) {
            const props = this.state.props;
            const PageComponent = this.state.pageComponent;
            return (react_1.default.createElement(PageComponent, Object.assign({ onRedirect: this.redirect }, props)));
        }
        return react_1.default.createElement(WelcomeScreen, null);
    }
    render() {
        return (react_1.default.createElement("div", { className: "data-management-main" },
            react_1.default.createElement(navigation_1.default, { onSelect: this.redirect, active: this.state.active }),
            react_1.default.createElement("div", { className: "dm-page-content" }, this.renderPageContent()),
            react_1.default.createElement(modals_1.ModalSpace, null)));
    }
}
exports["default"] = DataManagementMainScreen;
const WelcomeScreen = () => {
    return (react_1.default.createElement("div", { className: "dm-welcome-screen" },
        react_1.default.createElement("h4", null, "Zarz\u0105dzanie danymi aplikacji"),
        react_1.default.createElement("p", null, "Przejd\u017A pod jedn\u0105 z powy\u017Cszych zak\u0142adek aby wprowadza\u0107, modyfikowa\u0107 oraz usuwa\u0107 informacje.")));
};


/***/ }),

/***/ "./React/data-management/navigation.tsx":
/*!**********************************************!*\
  !*** ./React/data-management/navigation.tsx ***!
  \**********************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const classes_page_1 = __importDefault(__webpack_require__(/*! ./classes/classes-page */ "./React/data-management/classes/classes-page.tsx"));
const category_1 = __importDefault(__webpack_require__(/*! ./enums/category */ "./React/data-management/enums/category.ts"));
const rooms_page_1 = __importDefault(__webpack_require__(/*! ./rooms/rooms-page */ "./React/data-management/rooms/rooms-page.tsx"));
const staff_page_1 = __importDefault(__webpack_require__(/*! ./staff/staff-page */ "./React/data-management/staff/staff-page.tsx"));
const subjects_page_1 = __importDefault(__webpack_require__(/*! ./subjects/subjects-page */ "./React/data-management/subjects/subjects-page.tsx"));
__webpack_require__(/*! ./navigation.css */ "./React/data-management/navigation.css");
class DMNavigationBar extends react_1.default.Component {
    generateNavItems() {
        this.items = [
            this.createNavItem("Przedmioty", category_1.default.Subjects, subjects_page_1.default),
            this.createNavItem("Personel", category_1.default.Staff, staff_page_1.default),
            this.createNavItem("Pomieszczenia", category_1.default.Rooms, rooms_page_1.default),
            this.createNavItem("Klasy", category_1.default.Classes, classes_page_1.default),
        ];
    }
    createNavItem(label, category, pageComponent) {
        return {
            label: label,
            onClick: () => { this.props.onSelect(category, pageComponent); },
            active: this.props.active == category
        };
    }
    render() {
        this.generateNavItems();
        return (react_1.default.createElement("div", { className: "dm-navigation-bar" }, this.items.map(item => react_1.default.createElement(DMNavigationItem, { key: item.label, label: item.label, onClick: item.onClick, isActive: item.active }))));
    }
}
exports["default"] = DMNavigationBar;
class DMNavigationItem extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: `dm-navigation-item ${this.props.isActive ? "dm-navigation-item-active" : ""}`, onClick: () => this.props.onClick() }, this.props.label));
    }
}


/***/ }),

/***/ "./React/data-management/rooms/room-list.tsx":
/*!***************************************************!*\
  !*** ./React/data-management/rooms/room-list.tsx ***!
  \***************************************************/
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
const lists_1 = __webpack_require__(/*! ../../shared/lists */ "./React/shared/lists.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const room_mod_comp_1 = __importDefault(__webpack_require__(/*! ./room-mod-comp */ "./React/data-management/rooms/room-mod-comp.tsx"));
const RoomsList = (props) => {
    const columnsSetting = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        },
        {
            header: "Piętro",
            prop: "floor",
            style: { width: '10%' }
        }
    ];
    const loadAsync = () => __awaiter(void 0, void 0, void 0, function* () {
        let response = yield main_1.server.getAsync("RoomEntries");
        return response;
    });
    return (react_1.default.createElement(lists_1.List, { columnsSetting: columnsSetting, modificationComponent: room_mod_comp_1.default, loadDataAsync: loadAsync }));
};
exports["default"] = RoomsList;


/***/ }),

/***/ "./React/data-management/rooms/room-mod-comp.tsx":
/*!*******************************************************!*\
  !*** ./React/data-management/rooms/room-mod-comp.tsx ***!
  \*******************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const loader_1 = __importStar(__webpack_require__(/*! ../../shared/loader */ "./React/shared/loader.tsx"));
const validator_1 = __importDefault(__webpack_require__(/*! ../../shared/validator */ "./React/shared/validator.ts"));
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
class RoomModComp extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._validator = new validator_1.default();
        this.createOnChangeHandler = (property) => {
            return (event) => {
                const value = event.target.value;
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    data[property] = value;
                    return { data };
                });
                this.props.onMadeAnyChange();
            };
        };
        this.onSubmitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (this.state.data.name == "")
                this.state.data.name = this.state.defaultName;
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("RoomData", undefined, this.state.data);
            if (response.success)
                yield this.props.reloadAsync();
            else
                console.debug(response);
        });
        this.state = {
            awaitingData: true,
            data: {
                name: '',
                floor: 0
            }
        };
        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            floor: {
                notNull: true,
                other: (model, prop) => model[prop] < 0
                    ? {
                        error: 'Piętro musi mieć wartość większą niż 0',
                        on: prop
                    } : undefined
            },
            name: { notNull: true, notEmpty: 'Pole nie może być puste' }
        });
        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchDefaultNameAsync();
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let res = yield main_1.server.getAsync("RoomModificationData", {
                id: this.props.recordId
            });
            this.setState({ data: res.data, defaultName: res.defaultName, awaitingData: false });
        });
    }
    fetchDefaultNameAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            const name = yield main_1.server.getAsync("RoomDefaultName");
            this.setState({ defaultName: name, awaitingData: false });
        });
    }
    render() {
        if (this.state.awaitingData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.onSubmitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "name-input", label: "Nazwa", value: this.state.data.name, placeholder: this.state.defaultName, onChange: this.createOnChangeHandler('name'), errorMessages: this._validator.getErrorMsgsFor('name'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "number-input", label: "Numer", value: this.state.data.number, onChange: this.createOnChangeHandler('number'), errorMessages: this._validator.getErrorMsgsFor('number'), type: "number" }),
                react_1.default.createElement(form_controls_1.Input, { name: "floor-input", label: "Pi\u0119tro", value: this.state.data.floor, onChange: this.createOnChangeHandler('floor'), errorMessages: this._validator.getErrorMsgsFor('floor'), type: "number" }),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
}
exports["default"] = RoomModComp;


/***/ }),

/***/ "./React/data-management/rooms/rooms-page.tsx":
/*!****************************************************!*\
  !*** ./React/data-management/rooms/rooms-page.tsx ***!
  \****************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const room_list_1 = __importDefault(__webpack_require__(/*! ./room-list */ "./React/data-management/rooms/room-list.tsx"));
class RoomsPage extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: "dm-rooms-page" },
            react_1.default.createElement(room_list_1.default, null)));
    }
}
exports["default"] = RoomsPage;


/***/ }),

/***/ "./React/data-management/staff/staff-list.tsx":
/*!****************************************************!*\
  !*** ./React/data-management/staff/staff-list.tsx ***!
  \****************************************************/
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
const lists_1 = __webpack_require__(/*! ../../shared/lists */ "./React/shared/lists.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const staff_person_mod_comp_1 = __importDefault(__webpack_require__(/*! ./staff-person-mod-comp */ "./React/data-management/staff/staff-person-mod-comp.tsx"));
const StaffList = (props) => {
    const columnsSetting = [
        {
            header: "Imię i nazwisko",
            prop: "name",
        },
        {
            header: "Specjalizacja",
            prop: "specialization"
        }
    ];
    const loadAsync = () => __awaiter(void 0, void 0, void 0, function* () {
        let response = yield main_1.server.getAsync("StaffPersonsEntries");
        return response;
    });
    return (react_1.default.createElement(lists_1.GroupList, { columnsSetting: columnsSetting, loadDataAsync: loadAsync, modificationComponent: staff_person_mod_comp_1.default }));
};
exports["default"] = StaffList;


/***/ }),

/***/ "./React/data-management/staff/staff-page.tsx":
/*!****************************************************!*\
  !*** ./React/data-management/staff/staff-page.tsx ***!
  \****************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const staff_list_1 = __importDefault(__webpack_require__(/*! ./staff-list */ "./React/data-management/staff/staff-list.tsx"));
class StaffPage extends react_1.default.Component {
    render() {
        return (react_1.default.createElement(staff_list_1.default, null));
    }
}
exports["default"] = StaffPage;


/***/ }),

/***/ "./React/data-management/staff/staff-person-mod-comp.tsx":
/*!***************************************************************!*\
  !*** ./React/data-management/staff/staff-person-mod-comp.tsx ***!
  \***************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const loader_1 = __importStar(__webpack_require__(/*! ../../shared/loader */ "./React/shared/loader.tsx"));
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
class StaffPersonModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.createTextChangeHandler = (property) => {
            return (event) => {
                const value = event.target.value;
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    data[property] = value;
                    return { data };
                });
                this.props.onMadeAnyChange();
            };
        };
        this.createOnSubjectsChangeHandler = (property) => {
            return (values) => {
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    data[property] = values;
                    return { data };
                });
                this.props.onMadeAnyChange();
            };
        };
        this.submitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("StaffPersonData", undefined, Object.assign(Object.assign({}, this.state.data), { groupId: this.props.groupId }));
            if (response.success)
                yield this.props.reloadAsync();
            else
                console.debug(response);
        });
        this.state = {
            awaitingPersonData: this.props.recordId > 0,
            data: {
                firstName: '',
                secondName: '',
                lastName: ''
            },
            availableSubjects: []
        };
        this._validator.setRules({
            firstName: { notNull: true, notEmpty: true },
            lastName: { notNull: true, notEmpty: true },
            additionalSubjectsIds: {
                other: (model, prop) => {
                    var _a;
                    if (((_a = model.additionalSubjectsIds) === null || _a === void 0 ? void 0 : _a.length)
                        && model.additionalSubjectsIds.some(x => model.mainSubjectsIds.includes(x)))
                        return {
                            on: prop,
                            error: "Ten sam przedmiot nie może być jednocześnie głównym i dodatkowym"
                        };
                }
            }
        });
        if (this.props.recordId)
            this.fetchAsync();
        this.fetchSubjectsAsync();
    }
    render() {
        if (this.state.awaitingPersonData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.submitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "first-name-input", label: "Imi\u0119", value: this.state.data.firstName, onChange: this.createTextChangeHandler('firstName'), errorMessages: this._validator.getErrorMsgsFor('firstName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "second-name-input", label: "Drugie imi\u0119", value: this.state.data.secondName, onChange: this.createTextChangeHandler('secondName'), errorMessages: this._validator.getErrorMsgsFor('secondName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "last-name-input", label: "Nazwisko", value: this.state.data.lastName, onChange: this.createTextChangeHandler('lastName'), errorMessages: this._validator.getErrorMsgsFor('lastName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Multiselect, { name: "main-subejcts-input", label: "G\u0142\u00F3wne przedmioty", value: this._mainSubjectOptions, onChangeId: this.createOnSubjectsChangeHandler('mainSubjectsIds'), errorMessages: this._validator.getErrorMsgsFor('mainSubjectsIds'), options: this.state.availableSubjects.map(x => ({
                        label: x.name,
                        value: x.id
                    })) }),
                react_1.default.createElement(form_controls_1.Multiselect, { name: "additional-subejcts-input", label: "Dodatkowe przedmioty", value: this._additionalSubjectOptions, onChangeId: this.createOnSubjectsChangeHandler('additionalSubjectsIds'), errorMessages: this._validator.getErrorMsgsFor('additionalSubjectsIds'), options: this.state.availableSubjects.map(x => ({
                        label: x.name,
                        value: x.id
                    })) }),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let data = yield main_1.server.getAsync("StaffPersonDetails", {
                id: this.props.recordId,
                groupId: this.props.groupId
            });
            this.setState({ data, awaitingPersonData: false });
        });
    }
    fetchSubjectsAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let availableSubjects = yield main_1.server.getAsync("AvailableSubjects");
            this.setState({ availableSubjects });
        });
    }
    get _mainSubjectOptions() { return this.getSubjectOptions(this.state.data.mainSubjectsIds); }
    get _additionalSubjectOptions() { return this.getSubjectOptions(this.state.data.additionalSubjectsIds); }
    getSubjectOptions(from) {
        return this.state.availableSubjects.filter(x => from === null || from === void 0 ? void 0 : from.includes(x.id)).map(x => ({
            label: x.name,
            value: x.id
        }));
    }
}
exports["default"] = StaffPersonModComp;


/***/ }),

/***/ "./React/data-management/students/components/class-info-panel.tsx":
/*!************************************************************************!*\
  !*** ./React/data-management/students/components/class-info-panel.tsx ***!
  \************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const ClassInfoPanel = (props) => {
    return (react_1.default.createElement("div", { className: "dm-students-class-info-panel" },
        react_1.default.createElement("div", { className: "dm-cip-name" }, props.name),
        react_1.default.createElement("div", { className: "dm-cip-spec" }, props.specialization)));
};
exports["default"] = ClassInfoPanel;


/***/ }),

/***/ "./React/data-management/students/components/student-reg-rec-mod-comp.tsx":
/*!********************************************************************************!*\
  !*** ./React/data-management/students/components/student-reg-rec-mod-comp.tsx ***!
  \********************************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const loader_1 = __importStar(__webpack_require__(/*! ../../../shared/loader */ "./React/shared/loader.tsx"));
const validator_1 = __importDefault(__webpack_require__(/*! ../../../shared/validator */ "./React/shared/validator.ts"));
const main_1 = __webpack_require__(/*! ../../main */ "./React/data-management/main.tsx");
class StudentRegisterRecordModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.createOnTextChangeHandler = (property) => {
            return (event) => {
                const value = event.target.value;
                this.setStateFnData(data => data[property] = value);
            };
        };
        this.onAddressChange = (event) => {
            const value = event.target.value;
            this.setState(prevState => {
                const data = Object.assign({}, prevState.data);
                data.address = value;
                if (this.state.addressSameAsChilds.firstParent)
                    data.firstParent.address = value;
                if (this.state.addressSameAsChilds.secondParent && this.state.data.secondParent)
                    data.secondParent.address = value;
                return { data };
            });
        };
        this.onSubmitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("StudentRegisterRecordData", undefined, Object.assign({}, this.state.data));
            if (response.success) {
                yield this.props.reloadAsync();
                this.props.selectRecord(response.id);
                this.props.assignedAtPresenter.close();
            }
            else
                console.debug(response);
        });
        this.createParentOnTextChangeHandler = (parent, property) => {
            return (event) => {
                const value = event.target.value;
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    if (data[parent] == undefined)
                        data[parent] = EmptyParentRegisterSubrecordDetails();
                    data[parent][property] = value;
                    return { data };
                });
            };
        };
        this.createOnAddressSameAsChildsChangeHandler = (parent) => {
            return (event) => {
                const value = event.target.checked;
                this.setState(prevState => {
                    const data = Object.assign({}, prevState.data);
                    const addressSameAsChilds = Object.assign({}, prevState.addressSameAsChilds);
                    addressSameAsChilds[parent] = value;
                    if (value) {
                        data[parent].address = data.address;
                    }
                    return { data, addressSameAsChilds };
                });
            };
        };
        this.removeSecondParent = () => {
            this.setState(prevState => {
                const data = Object.assign({}, prevState.data);
                data.secondParent = undefined;
                return { data };
            });
        };
        this.addSecondParent = () => {
            this.setState(prevState => {
                const data = Object.assign({}, prevState.data);
                data.secondParent = EmptyParentRegisterSubrecordDetails();
                return { data };
            });
        };
        this.parentInputs = (parent, parentProp, validatorGetter) => {
            var _a, _b, _c, _d, _e, _f;
            if (parent == undefined)
                return (react_1.default.createElement(react_1.default.Fragment, null,
                    react_1.default.createElement("button", { onClick: this.addSecondParent }, "Dodaj drugiego rodzica")));
            return (react_1.default.createElement("div", { className: "col" },
                react_1.default.createElement("h3", null,
                    "Rodzic #",
                    parentProp == "firstParent" ? 1 : 2),
                parentProp == "firstParent" ? react_1.default.createElement(react_1.default.Fragment, null)
                    : (react_1.default.createElement("button", { onClick: this.removeSecondParent }, "Usu\u0144")),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-first-name-input`, label: "Imi\u0119", value: parent.firstName, onChange: this.createParentOnTextChangeHandler(parentProp, 'firstName'), errorMessages: (_a = validatorGetter()) === null || _a === void 0 ? void 0 : _a.getErrorMsgsFor('firstName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-second-name-input`, label: "Drugie imi\u0119", value: parent.secondName, onChange: this.createParentOnTextChangeHandler(parentProp, 'secondName'), errorMessages: (_b = validatorGetter()) === null || _b === void 0 ? void 0 : _b.getErrorMsgsFor('secondName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-last-name-input`, label: "Nazwisko", value: parent.lastName, onChange: this.createParentOnTextChangeHandler(parentProp, 'lastName'), errorMessages: (_c = validatorGetter()) === null || _c === void 0 ? void 0 : _c.getErrorMsgsFor('lastName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-address-input`, label: "Adres zamieszkania", value: parent.address, disabled: this.state.addressSameAsChilds[parentProp], onChange: this.createParentOnTextChangeHandler(parentProp, 'address'), errorMessages: (_d = validatorGetter()) === null || _d === void 0 ? void 0 : _d.getErrorMsgsFor('address'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { inputClassName: "form-check-input", name: `${parentProp}-address-same-as-childs-input`, label: "Adres taki sam jak ucznia", checked: this.state.addressSameAsChilds[parentProp], onChange: this.createOnAddressSameAsChildsChangeHandler(parentProp), type: "checkbox" }),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-phone-number-input`, label: "Numer telefonu", value: parent.phoneNumber, onChange: this.createParentOnTextChangeHandler(parentProp, 'phoneNumber'), errorMessages: (_e = validatorGetter()) === null || _e === void 0 ? void 0 : _e.getErrorMsgsFor('phoneNumber'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: `${parentProp}-email-input`, label: "Email", value: parent.email, onChange: this.createParentOnTextChangeHandler(parentProp, 'email'), errorMessages: (_f = validatorGetter()) === null || _f === void 0 ? void 0 : _f.getErrorMsgsFor('email'), type: "text" })));
        };
        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                firstName: '',
                secondName: '',
                lastName: '',
                dateOfBirth: '',
                address: '',
                personalId: '',
                placeOfBirth: '',
                firstParent: EmptyParentRegisterSubrecordDetails(),
                secondParent: EmptyParentRegisterSubrecordDetails()
            },
            addressSameAsChilds: {
                firstParent: true,
                secondParent: true
            }
        };
        this._validator.setRules({
            firstName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            lastName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            address: { notNull: true, notEmpty: 'Pole nie może być puste' },
            dateOfBirth: { notNull: true, validDate: true },
            personalId: { notNull: true, notEmpty: 'Pole nie może być puste' },
            placeOfBirth: { notNull: true, notEmpty: 'Pole nie może być puste' },
            firstParent: {
                notNull: true,
                other: (model, prop) => {
                    if (!model[prop])
                        return undefined;
                    this._firstParentValidator = this._parentValidator;
                    this._firstParentValidator.forModel(model[prop]);
                    return this._firstParentValidator.validate()
                        ? undefined
                        : {
                            error: "Nieprawidłowe dane rodzica",
                            on: prop
                        };
                }
            },
            secondParent: {
                other: (model, prop) => {
                    if (!model[prop])
                        return undefined;
                    this._secondParentValidator = this._parentValidator;
                    this._secondParentValidator.forModel(model[prop]);
                    return this._secondParentValidator.validate()
                        ? undefined
                        : {
                            error: "Nieprawidłowe dane rodzica",
                            on: prop
                        };
                }
            }
        });
        if (this.props.recordId)
            this.fetchAsync();
    }
    get _parentValidator() {
        const validator = new validator_1.default();
        validator.setRules({
            firstName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            lastName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            address: { notNull: true, notEmpty: 'Pole nie może być puste' },
            phoneNumber: { notNull: true, notEmpty: 'Pole nie może być puste' }
        });
        return validator;
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let response = yield main_1.server.getAsync("StudentRegisterRecordModificationData", {
                id: this.props.recordId
            });
            this.setState({
                data: response.data,
                awaitingData: false,
                addressSameAsChilds: {
                    firstParent: response.data.address == response.data.firstParent.address,
                    secondParent: response.data.secondParent == undefined ? false
                        : response.data.address == response.data.secondParent.address
                }
            });
        });
    }
    render() {
        if (this.state.awaitingData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.onSubmitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "first-name-input", label: "Imi\u0119", value: this.state.data.firstName, onChange: this.createOnTextChangeHandler('firstName'), errorMessages: this._validator.getErrorMsgsFor('firstName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "second-name-input", label: "Drugie imi\u0119", value: this.state.data.secondName, onChange: this.createOnTextChangeHandler('secondName'), errorMessages: this._validator.getErrorMsgsFor('secondName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "last-name-input", label: "Nazwisko", value: this.state.data.lastName, onChange: this.createOnTextChangeHandler('lastName'), errorMessages: this._validator.getErrorMsgsFor('lastName'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "date-of-birth-input", label: "Data urodzenia", value: this.state.data.dateOfBirth, onChange: this.createOnTextChangeHandler('dateOfBirth'), errorMessages: this._validator.getErrorMsgsFor('dateOfBirth'), type: "date" }),
                react_1.default.createElement(form_controls_1.Input, { name: "place-of-birth-input", label: "Miejsce urodzenia", value: this.state.data.placeOfBirth, onChange: this.createOnTextChangeHandler('placeOfBirth'), errorMessages: this._validator.getErrorMsgsFor('placeOfBirth'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "personal-id-input", label: "Numer identyfikacyjny (np. PESEL)", value: this.state.data.personalId, onChange: this.createOnTextChangeHandler('personalId'), errorMessages: this._validator.getErrorMsgsFor('personalId'), type: "text" }),
                react_1.default.createElement(form_controls_1.Input, { name: "address-input", label: "Adres zamieszkania", value: this.state.data.address, onChange: this.onAddressChange, errorMessages: this._validator.getErrorMsgsFor('address'), type: "text" }),
                react_1.default.createElement("div", { className: "container" },
                    react_1.default.createElement("div", { className: "row align-items-start" },
                        this.parentInputs(this.state.data.firstParent, "firstParent", () => this._firstParentValidator),
                        this.parentInputs(this.state.data.secondParent, "secondParent", () => this._secondParentValidator))),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
}
exports["default"] = StudentRegisterRecordModComp;
function EmptyParentRegisterSubrecordDetails() {
    return {
        firstName: '',
        secondName: '',
        lastName: '',
        address: '',
        phoneNumber: '',
        email: ''
    };
}


/***/ }),

/***/ "./React/data-management/students/student-list.tsx":
/*!*********************************************************!*\
  !*** ./React/data-management/students/student-list.tsx ***!
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
const lists_1 = __webpack_require__(/*! ../../shared/lists */ "./React/shared/lists.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const student_mod_comp_1 = __importDefault(__webpack_require__(/*! ./student-mod-comp */ "./React/data-management/students/student-mod-comp.tsx"));
const StudentsList = (props) => {
    const columnsSetting = [
        {
            header: "Numer",
            prop: 'numberInJournal',
            style: {
                width: '20px'
            }
        },
        {
            header: "Imię i nazwisko",
            prop: "name",
        }
    ];
    const loadAsync = () => __awaiter(void 0, void 0, void 0, function* () {
        let response = yield main_1.server.getAsync("StudentEntries", {
            classId: props.classId
        });
        return [prepareStudentsData(response)];
    });
    const prepareStudentsData = (received) => {
        const highestJournalNr = findHightestNr(received.map(x => x.numberInJournal));
        const data = [];
        for (let i = 1; i <= highestJournalNr; i++) {
            const existing = received.find(x => x.numberInJournal == i);
            if (existing)
                data.push(existing);
            else
                data.push({
                    numberInJournal: i,
                    id: 0,
                    name: '',
                });
        }
        return {
            id: props.classId,
            entries: data
        };
    };
    const findHightestNr = (numbers) => {
        let highest = 0;
        for (const nr of numbers)
            highest = highest < nr ? nr : highest;
        return highest;
    };
    return (react_1.default.createElement(lists_1.GroupList, { columnsSetting: columnsSetting, loadDataAsync: loadAsync, modificationComponent: student_mod_comp_1.default }));
};
exports["default"] = StudentsList;


/***/ }),

/***/ "./React/data-management/students/student-mod-comp.tsx":
/*!*************************************************************!*\
  !*** ./React/data-management/students/student-mod-comp.tsx ***!
  \*************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const loader_1 = __importStar(__webpack_require__(/*! ../../shared/loader */ "./React/shared/loader.tsx"));
const modals_1 = __webpack_require__(/*! ../../shared/modals */ "./React/shared/modals.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const student_reg_rec_mod_comp_1 = __importDefault(__webpack_require__(/*! ./components/student-reg-rec-mod-comp */ "./React/data-management/students/components/student-reg-rec-mod-comp.tsx"));
class StudentModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.refetchRegisterRecords = () => __awaiter(this, void 0, void 0, function* () {
            this.setState({ awaitingData: true });
            yield this.fetchRegisterRecords();
        });
        this.changeNumberInJournal = (event) => {
            const value = event.target.value;
            this.setStateFnData(data => data.numberInJournal = value);
            this.props.onMadeAnyChange();
        };
        this.onRegisterRecordChangeHandler = (id) => {
            const setRecord = () => this.setStateFnData(data => data.registerRecordId = id);
            const selected = this.state.registerRecords.find(x => x.id == id);
            if ((selected === null || selected === void 0 ? void 0 : selected.className) != undefined) {
                modals_1.modalController.addConfirmation({
                    header: "Ten uczeń jest już przypisany do klasy",
                    text: `Ten uczeń jest przypisany do klasy ${selected.className}. Czy chcesz przepisać go do tej?`,
                    onConfirm: () => {
                        setRecord();
                    }
                });
            }
            else
                setRecord();
            this.props.onMadeAnyChange();
        };
        this.openStudentRegisterRecordMCForCreation = () => {
            modals_1.modalController.addCustomComponent({
                modificationComponent: student_reg_rec_mod_comp_1.default,
                modificationComponentProps: {
                    reloadAsync: this.refetchRegisterRecords,
                    selectRecord: id => this.setStateFnData(data => data.registerRecordId = id)
                },
                style: {
                    width: '500px'
                }
            });
        };
        this.openStudentRegisterRecordMCForModification = () => {
            if (!this.state.data.registerRecordId)
                return;
            modals_1.modalController.addCustomComponent({
                modificationComponent: student_reg_rec_mod_comp_1.default,
                modificationComponentProps: {
                    recordId: this.state.data.registerRecordId,
                    reloadAsync: this.refetchRegisterRecords,
                    selectRecord: id => this.setStateFnData(data => data.registerRecordId = id)
                }
            });
        };
        this.onSubmitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("StudentData", undefined, Object.assign({}, this.state.data));
            if (response.success)
                yield this.props.reloadAsync();
            else
                console.debug(response);
        });
        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                organizationalClassId: this.props.groupId
            },
            registerRecords: []
        };
        this._validator.setRules({
            numberInJournal: {
                notNull: true,
                other: (model, prop) => (model.numberInJournal < 1
                    ? { error: 'Numer w dzienniku musi być większy od 0', on: prop }
                    : undefined)
            },
            registerRecordId: { notNull: true }
        });
        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchRegisterRecords();
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let response = yield main_1.server.getAsync("StudentModificationData", {
                id: this.props.recordId
            });
            this.setState({
                data: response.data,
                registerRecords: response.registerRecords,
                awaitingData: false
            });
        });
    }
    fetchRegisterRecords() {
        return __awaiter(this, void 0, void 0, function* () {
            let response = yield main_1.server.getAsync("StudentRegisterRecordEntries");
            this.setState({ registerRecords: response, awaitingData: false });
        });
    }
    render() {
        if (this.state.awaitingData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.onSubmitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "number-in-journal-input", label: "Numer w dzienniku", value: this.state.data.numberInJournal, onChange: this.changeNumberInJournal, errorMessages: this._validator.getErrorMsgsFor('numberInJournal'), type: "number" }),
                react_1.default.createElement(form_controls_1.Select, { name: "register-record-input", label: "Dane ucznia", value: this.state.data.registerRecordId, onChangeId: this.onRegisterRecordChangeHandler, options: this.state.registerRecords.map(x => ({
                        label: x.name,
                        value: x.id
                    })), errorMessages: this._validator.getErrorMsgsFor('registerRecordId') }),
                this.state.data.registerRecordId != undefined
                    ? (react_1.default.createElement("button", { type: "button", onClick: this.openStudentRegisterRecordMCForModification }, "Edytuj dane ucznia")) : undefined,
                react_1.default.createElement("button", { type: "button", onClick: this.openStudentRegisterRecordMCForCreation }, "Dodaj nowego ucznia"),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
}
exports["default"] = StudentModComp;


/***/ }),

/***/ "./React/data-management/students/students-page.tsx":
/*!**********************************************************!*\
  !*** ./React/data-management/students/students-page.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const class_info_panel_1 = __importDefault(__webpack_require__(/*! ./components/class-info-panel */ "./React/data-management/students/components/class-info-panel.tsx"));
const student_list_1 = __importDefault(__webpack_require__(/*! ./student-list */ "./React/data-management/students/student-list.tsx"));
__webpack_require__(/*! ./students.css */ "./React/data-management/students/students.css");
class StudentsPage extends react_1.default.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (react_1.default.createElement(react_1.default.Fragment, null,
            react_1.default.createElement(class_info_panel_1.default, { name: this.props.className, specialization: this.props.classSpecialization }),
            react_1.default.createElement(student_list_1.default, { classId: this.props.classId })));
    }
}
exports["default"] = StudentsPage;


/***/ }),

/***/ "./React/data-management/subjects/subject-list.tsx":
/*!*********************************************************!*\
  !*** ./React/data-management/subjects/subject-list.tsx ***!
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
const lists_1 = __webpack_require__(/*! ../../shared/lists */ "./React/shared/lists.ts");
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
const subject_mod_comp_1 = __importDefault(__webpack_require__(/*! ./subject-mod-comp */ "./React/data-management/subjects/subject-mod-comp.tsx"));
const SubjectList = (props) => {
    const columnsSetting = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        }
    ];
    const loadAsync = () => __awaiter(void 0, void 0, void 0, function* () {
        let response = yield main_1.server.getAsync("SubjectEntries");
        return response;
    });
    return (react_1.default.createElement(lists_1.List, { columnsSetting: columnsSetting, modificationComponent: subject_mod_comp_1.default, loadDataAsync: loadAsync }));
};
exports["default"] = SubjectList;


/***/ }),

/***/ "./React/data-management/subjects/subject-mod-comp.tsx":
/*!*************************************************************!*\
  !*** ./React/data-management/subjects/subject-mod-comp.tsx ***!
  \*************************************************************/
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
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
const loader_1 = __importStar(__webpack_require__(/*! ../../shared/loader */ "./React/shared/loader.tsx"));
const validator_1 = __importDefault(__webpack_require__(/*! ../../shared/validator */ "./React/shared/validator.ts"));
const main_1 = __webpack_require__(/*! ../main */ "./React/data-management/main.tsx");
class SubjectModComp extends react_1.default.Component {
    constructor(props) {
        super(props);
        this._validator = new validator_1.default();
        this.onNameChange = (event) => {
            const value = event.target.value;
            this.setState(prevState => {
                const data = Object.assign({}, prevState.data);
                data.name = value;
                return { data };
            });
            this.props.onMadeAnyChange();
        };
        this.onSubmitAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            let response = yield main_1.server.postAsync("SubjectData", undefined, this.state.data);
            if (response.success)
                yield this.props.reloadAsync();
            else
                console.debug(response);
        });
        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                name: ''
            }
        };
        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            name: { notNull: true, notEmpty: "Nazwa nie może być pusta." }
        });
        if (this.props.recordId)
            this.fetchAsync();
    }
    fetchAsync() {
        return __awaiter(this, void 0, void 0, function* () {
            let data = yield main_1.server.getAsync("SubjectDetails", {
                id: this.props.recordId
            });
            this.setState({ data, awaitingData: false });
        });
    }
    render() {
        if (this.state.awaitingData)
            return (react_1.default.createElement(loader_1.default, { enable: true, size: loader_1.LoaderSize.Medium, type: loader_1.LoaderType.DivWholeSpace }));
        return (react_1.default.createElement("div", null,
            react_1.default.createElement("form", { onSubmit: this.onSubmitAsync },
                react_1.default.createElement(form_controls_1.Input, { name: "name-input", label: "Nazwa", value: this.state.data.name, onChange: this.onNameChange, errorMessages: this._validator.getErrorMsgsFor('name'), type: "text" }),
                react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" }))));
    }
}
exports["default"] = SubjectModComp;


/***/ }),

/***/ "./React/data-management/subjects/subjects-page.tsx":
/*!**********************************************************!*\
  !*** ./React/data-management/subjects/subjects-page.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const subject_list_1 = __importDefault(__webpack_require__(/*! ./subject-list */ "./React/data-management/subjects/subject-list.tsx"));
class SubjectsPage extends react_1.default.Component {
    render() {
        return (react_1.default.createElement("div", { className: "dm-subjects-page" },
            react_1.default.createElement(subject_list_1.default, null)));
    }
}
exports["default"] = SubjectsPage;


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


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared"], () => (__webpack_exec__("./React/data-management.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=data_management.bundle.js.map