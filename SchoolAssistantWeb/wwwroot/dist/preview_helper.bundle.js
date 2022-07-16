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

/***/ "./React/preview-helper/components/page-explanations/explanation-block.css":
/*!*********************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/explanation-block.css ***!
  \*********************************************************************************/
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

/***/ "./React/shared/loader/page-blocking-loader.css":
/*!******************************************************!*\
  !*** ./React/shared/loader/page-blocking-loader.css ***!
  \******************************************************/
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
const components_1 = __webpack_require__(/*! ../../shared/components */ "./React/shared/components.ts");
const page_blocking_loader_1 = __importDefault(__webpack_require__(/*! ../../shared/loader/page-blocking-loader */ "./React/shared/loader/page-blocking-loader.tsx"));
const modals_1 = __webpack_require__(/*! ../../shared/modals */ "./React/shared/modals.ts");
const server_connection_1 = __importDefault(__webpack_require__(/*! ../../shared/server-connection */ "./React/shared/server-connection.tsx"));
__webpack_require__(/*! ./default-menu.css */ "./React/preview-helper/components/default-menu.css");
const DefaultMenu = (props) => {
    const loaderRef = react_1.default.createRef();
    const resetAppDataAsync = () => {
        modals_1.modalController.addConfirmation({
            header: "Resetowanie wszystkich danych",
            text: "Potwierdzenie wiąże się z nieodwracalnym napisaniem wszystkich danych aplikacji",
            onConfirm: () => __awaiter(void 0, void 0, void 0, function* () {
                loaderRef.current.show();
                var res = yield resetAppDataServer.getResponseAsync(null);
                loaderRef.current.hide();
                if (res.ok)
                    window.location.reload();
            })
        });
    };
    return (react_1.default.createElement("div", { className: "ph-default-menu" },
        react_1.default.createElement(components_1.ActionButton, { label: "Zresetuj dane aplikacji", className: "ph-default-menu-button", onClick: resetAppDataAsync }),
        react_1.default.createElement("p", null, "Zresetuj przedmioty, nauczycieli, pomieszczenia, klasy, uczni\u00F3w, oceny oraz zaj\u0119cia do stanu stanu pocz\u0105tkowego."),
        props.children,
        react_1.default.createElement(page_blocking_loader_1.default, { ref: loaderRef })));
};
exports["default"] = DefaultMenu;
const resetAppDataServer = new server_connection_1.default("/ResetDatabsePreview");


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
const components_1 = __webpack_require__(/*! ../../shared/components */ "./React/shared/components.ts");
const form_controls_1 = __webpack_require__(/*! ../../shared/form-controls */ "./React/shared/form-controls.ts");
__webpack_require__(/*! ./login-menu.css */ "./React/preview-helper/components/login-menu.css");
const LoginMenu = (props) => {
    return (react_1.default.createElement("div", { className: "ph-login-menu" },
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania administratora", userName: props.logins.administratorUserName, password: props.logins.administratorPassword }),
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania przyk\u0142adowego nauczyciela", userName: props.logins.teacherUserName, password: props.logins.teacherPassword }),
        react_1.default.createElement(DisplayCredentials, { header: "Dane logowania przyk\u0142adowego ucznia", userName: props.logins.studentUserName, password: props.logins.studentPassword })));
};
exports["default"] = LoginMenu;
const DisplayCredentials = ({ header, userName, password }) => {
    if (!userName || !password)
        return react_1.default.createElement(react_1.default.Fragment, null);
    return (react_1.default.createElement(react_1.default.Fragment, null,
        react_1.default.createElement("p", { className: "ph-login-menu-header" }, header),
        react_1.default.createElement(form_controls_1.LabelValue, { label: "Nazwa u\u017Cytkownika", value: react_1.default.createElement(CopyableValue, { value: userName }), width: 300 }),
        react_1.default.createElement(form_controls_1.LabelValue, { label: "Has\u0142o", value: react_1.default.createElement(CopyableValue, { value: password }), width: 300 })));
};
const CopyableValue = ({ value }) => {
    const copyToClipboard = () => {
        navigator.clipboard.writeText(value);
    };
    return (react_1.default.createElement(react_1.default.Fragment, null,
        react_1.default.createElement("span", null, value),
        react_1.default.createElement(components_1.IconButton, { onClick: copyToClipboard, faIcon: "fa-solid fa-copy", label: "Kopiuj do schowka", className: "ph-copy-to-clipboard-btn" })));
};


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/create-user-explanation.tsx":
/*!***************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/create-user-explanation.tsx ***!
  \***************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const CreateUserExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "Na tej stronie mo\u017Cna utworzy\u0107 u\u017Cytkownika po\u0142\u0105czonego z danym Uczniem/Nauczycielem. Po wybraniu z listy wprowad\u017A odpowiednie dane i utw\u00F3rz u\u017Cytkownika. Po utworzeniu wygenerowane zostanie has\u0142o (aplikacja obecnie nie korzysta z klienta poczty, dlatego maile nie s\u0105 wysy\u0142ane)."));
exports["default"] = CreateUserExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/data-management-explanation.tsx":
/*!*******************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/data-management-explanation.tsx ***!
  \*******************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const DataManagementExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "W tym miejscu uprawnieni u\u017Cytkownicy (obecnie tylko Administrator) ma dost\u0119p do zarz\u0105dzania danymi w aplikacji. Przejd\u017A w odpowiedni\u0105 zak\u0142adk\u0119 i zacznij dodawa\u0107 oraz modyfikowa\u0107 dane."));
exports["default"] = DataManagementExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/explanation-block.tsx":
/*!*********************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/explanation-block.tsx ***!
  \*********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
__webpack_require__(/*! ./explanation-block.css */ "./React/preview-helper/components/page-explanations/explanation-block.css");
const ExplanationBlock = ({ children }) => (react_1.default.createElement("p", { className: "explanation-block" }, children));
exports["default"] = ExplanationBlock;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/index-explanation.tsx":
/*!*********************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/index-explanation.tsx ***!
  \*********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const IndexExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "Ta strona jest widoczna tylko dla u\u017Cytkownika o roli Administratora."));
exports["default"] = IndexExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/schedule-arranger-explanation.tsx":
/*!*********************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/schedule-arranger-explanation.tsx ***!
  \*********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const ScheduleArrangerExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "W tym miejscu odbywa si\u0119 projektowanie planu lekcji. Na pocz\u0105tek przejd\u017A do wybranej klasy. Aby wstawia\u0107 zaj\u0119cia przeci\u0105gnij i upu\u015B\u0107 je w wybranym miejscu na planie lekcji. Mo\u017Cesz r\u00F3wnie\u017C po prostu nacisn\u0105\u0107 na zaj\u0119cia a p\u00F3\u017Aniej na plan lekcji. Aby doda\u0107 nowy typ zaj\u0119\u0107 do wstawiania naci\u015Bnij przycisk z ikon\u0105 \u201Eplusa\u201D. Przejd\u017A do edycji zaj\u0119\u0107 naciskaj\u0105c na nie."));
exports["default"] = ScheduleArrangerExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/scheduled-lessons-explanation.tsx":
/*!*********************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/scheduled-lessons-explanation.tsx ***!
  \*********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const ScheduledLessonsExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "Na tej stronie Nauczyciel ma dost\u0119p do listy nadchodz\u0105cych i minionych zaj\u0119\u0107. Po lewej stronie znajduje si\u0119 panel filtracji (niezaimplementowane) oraz pobierania wcze\u015Bniejszych/nast\u0119pnych zaj\u0119\u0107. Aby rozpocz\u0105\u0107 prowadzenie zaj\u0119\u0107, lub modyfikowa\u0107 informacje ju\u017C odbytych/nadchodz\u0105cych naci\u015Bnij przycisk przy odpowiednich zaj\u0119ciach na li\u015Bcie."));
exports["default"] = ScheduledLessonsExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/student-dashboard-explanation.tsx":
/*!*********************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/student-dashboard-explanation.tsx ***!
  \*********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const StudentDashboardExplanation = () => (react_1.default.createElement(explanation_block_1.default, null,
    "Ta strona r\u00F3\u017Cni si\u0119 w zale\u017Cno\u015Bci od tego, jaki u\u017Cytkownik obecnie korzysta z aplikacji.",
    react_1.default.createElement("br", null),
    "Dla Ucznia widoczny jest obecnie wy\u0142\u0105cznie podgl\u0105d planu lekcji. W przysz\u0142o\u015Bci mo\u017Ce tu by\u0107 r\u00F3wnie\u017C zaprezentowana skr\u00F3towa informacja na temat otrzymanych przez Ucznia ocen."));
exports["default"] = StudentDashboardExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/teacher-dashboard-explanation.tsx":
/*!*********************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/teacher-dashboard-explanation.tsx ***!
  \*********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const TeacherDashboardExplanation = () => (react_1.default.createElement(explanation_block_1.default, null,
    "Ta strona r\u00F3\u017Cni si\u0119 w zale\u017Cno\u015Bci od tego, jaki u\u017Cytkownik obecnie korzysta z aplikacji.",
    react_1.default.createElement("br", null),
    "Dla Nauczyciela widoczny jest podgl\u0105d planu lekcji oraz rozpiska nadchodz\u0105cych zaj\u0119\u0107. Aby przegl\u0105da\u0107 wszystkie nadchodz\u0105ce oraz minione zaj\u0119cia naci\u015Bnij \u2018Poka\u017C wi\u0119cej\u2019, znajduj\u0105ce si\u0119 poni\u017Cej listy zaj\u0119\u0107. Aby rozpocz\u0105\u0107 prowadzenie zaj\u0119\u0107, lub modyfikowa\u0107 informacje ju\u017C odbytych/nadchodz\u0105cych naci\u015Bnij przycisk przy odpowiednich zaj\u0119ciach na li\u015Bcie."));
exports["default"] = TeacherDashboardExplanation;


/***/ }),

/***/ "./React/preview-helper/components/page-explanations/user-management-explanation.tsx":
/*!*******************************************************************************************!*\
  !*** ./React/preview-helper/components/page-explanations/user-management-explanation.tsx ***!
  \*******************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const explanation_block_1 = __importDefault(__webpack_require__(/*! ./explanation-block */ "./React/preview-helper/components/page-explanations/explanation-block.tsx"));
const UserManagementExplanation = () => (react_1.default.createElement(explanation_block_1.default, null, "Tu dost\u0119pna jest rozpiska u\u017Cytkownik\u00F3w wprowadzonych do systemu."));
exports["default"] = UserManagementExplanation;


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
    PreviewMenuType[PreviewMenuType["IndexPage"] = 1] = "IndexPage";
    PreviewMenuType[PreviewMenuType["TeacherDashboard"] = 2] = "TeacherDashboard";
    PreviewMenuType[PreviewMenuType["StudentDashboard"] = 3] = "StudentDashboard";
    PreviewMenuType[PreviewMenuType["ScheduledLessonsPage"] = 4] = "ScheduledLessonsPage";
    PreviewMenuType[PreviewMenuType["DataManagementPage"] = 5] = "DataManagementPage";
    PreviewMenuType[PreviewMenuType["ScheduleArrangerPage"] = 6] = "ScheduleArrangerPage";
    PreviewMenuType[PreviewMenuType["UserManagementPage"] = 7] = "UserManagementPage";
    PreviewMenuType[PreviewMenuType["CreateUserPage"] = 8] = "CreateUserPage";
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
const modals_1 = __webpack_require__(/*! ../shared/modals */ "./React/shared/modals.ts");
const default_menu_1 = __importDefault(__webpack_require__(/*! ./components/default-menu */ "./React/preview-helper/components/default-menu.tsx"));
const floating_pin_1 = __importDefault(__webpack_require__(/*! ./components/floating-pin */ "./React/preview-helper/components/floating-pin.tsx"));
const login_menu_1 = __importDefault(__webpack_require__(/*! ./components/login-menu */ "./React/preview-helper/components/login-menu.tsx"));
const create_user_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/create-user-explanation */ "./React/preview-helper/components/page-explanations/create-user-explanation.tsx"));
const data_management_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/data-management-explanation */ "./React/preview-helper/components/page-explanations/data-management-explanation.tsx"));
const index_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/index-explanation */ "./React/preview-helper/components/page-explanations/index-explanation.tsx"));
const schedule_arranger_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/schedule-arranger-explanation */ "./React/preview-helper/components/page-explanations/schedule-arranger-explanation.tsx"));
const scheduled_lessons_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/scheduled-lessons-explanation */ "./React/preview-helper/components/page-explanations/scheduled-lessons-explanation.tsx"));
const student_dashboard_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/student-dashboard-explanation */ "./React/preview-helper/components/page-explanations/student-dashboard-explanation.tsx"));
const teacher_dashboard_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/teacher-dashboard-explanation */ "./React/preview-helper/components/page-explanations/teacher-dashboard-explanation.tsx"));
const user_management_explanation_1 = __importDefault(__webpack_require__(/*! ./components/page-explanations/user-management-explanation */ "./React/preview-helper/components/page-explanations/user-management-explanation.tsx"));
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
            this.renderMenu(),
            react_1.default.createElement(modals_1.ModalSpace, null)));
    }
    renderMenu() {
        var _a;
        return (0, enum_help_1.enumAssignSwitch)(preview_menu_type_1.default, (_a = this.props) === null || _a === void 0 ? void 0 : _a.type, {
            LoginMenu: () => this.props.logins != undefined
                ? react_1.default.createElement(login_menu_1.default, { logins: this.props.logins })
                : react_1.default.createElement(default_menu_1.default, null),
            IndexPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(index_explanation_1.default, null)),
            TeacherDashboard: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(teacher_dashboard_explanation_1.default, null)),
            StudentDashboard: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(student_dashboard_explanation_1.default, null)),
            ScheduledLessonsPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(scheduled_lessons_explanation_1.default, null)),
            DataManagementPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(data_management_explanation_1.default, null)),
            ScheduleArrangerPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(schedule_arranger_explanation_1.default, null)),
            UserManagementPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(user_management_explanation_1.default, null)),
            CreateUserPage: react_1.default.createElement(default_menu_1.default, null,
                react_1.default.createElement(create_user_explanation_1.default, null)),
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


/***/ }),

/***/ "./React/shared/loader/page-blocking-loader.tsx":
/*!******************************************************!*\
  !*** ./React/shared/loader/page-blocking-loader.tsx ***!
  \******************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const loader_size_1 = __importDefault(__webpack_require__(/*! ./enums/loader-size */ "./React/shared/loader/enums/loader-size.ts"));
const loader_type_1 = __importDefault(__webpack_require__(/*! ./enums/loader-type */ "./React/shared/loader/enums/loader-type.ts"));
const loader_1 = __importDefault(__webpack_require__(/*! ./loader */ "./React/shared/loader/loader.tsx"));
__webpack_require__(/*! ./page-blocking-loader.css */ "./React/shared/loader/page-blocking-loader.css");
const PageBlockingLoader = react_1.default.forwardRef((props, ref) => {
    var _a;
    const className = "page-blocking-loader " + ((_a = props.className) !== null && _a !== void 0 ? _a : "");
    return (react_1.default.createElement(loader_1.default, { size: loader_size_1.default.Large, type: loader_type_1.default.BlockPage, className: className, enable: props.enable, ref: ref }));
});
exports["default"] = PageBlockingLoader;


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared"], () => (__webpack_exec__("./React/preview-helper.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=preview_helper.bundle.js.map