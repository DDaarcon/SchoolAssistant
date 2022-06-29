"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["schedule_arranger"],{

/***/ "./React/schedule-arranger/class-selector.css":
/*!****************************************************!*\
  !*** ./React/schedule-arranger/class-selector.css ***!
  \****************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/schedule-arranger-page.css":
/*!***********************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/schedule-arranger-page.css ***!
  \***********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab.css":
/*!***********************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab.css ***!
  \***********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.css":
/*!*****************************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.css ***!
  \*****************************************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-tiles.css":
/*!**********************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lesson-tiles.css ***!
  \**********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/other-lesson-tiles.css":
/*!****************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/other-lesson-tiles.css ***!
  \****************************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-shared/components/time-column.css":
/*!**********************************************************!*\
  !*** ./React/schedule-shared/components/time-column.css ***!
  \**********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-arranger.ts":
/*!************************************!*\
  !*** ./React/schedule-arranger.ts ***!
  \************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const main_1 = __importDefault(__webpack_require__(/*! ./schedule-arranger/main */ "./React/schedule-arranger/main.tsx"));
globalThis.Components.ScheduleArranger = main_1.default;


/***/ }),

/***/ "./React/schedule-arranger/class-selector.tsx":
/*!****************************************************!*\
  !*** ./React/schedule-arranger/class-selector.tsx ***!
  \****************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const React = __webpack_require__(/*! react */ "./node_modules/react/index.js");
const main_1 = __webpack_require__(/*! ./main */ "./React/schedule-arranger/main.tsx");
const schedule_arranger_page_1 = __importDefault(__webpack_require__(/*! ./schedule-arranger-page */ "./React/schedule-arranger/schedule-arranger-page.tsx"));
__webpack_require__(/*! ./class-selector.css */ "./React/schedule-arranger/class-selector.css");
class ScheduleClassSelectorPage extends React.Component {
    render() {
        return (React.createElement("div", { className: "sa-selector-classes" },
            React.createElement("h2", null, "Wybierz klas\u0119"),
            this.props.entries.map(entry => React.createElement(ClassEntry, Object.assign({ key: entry.id }, entry)))));
    }
}
exports["default"] = ScheduleClassSelectorPage;
const ClassEntry = (props) => {
    const selectClass = () => main_1.server.getAsync("ClassLessons", { classId: props.id })
        .then((result) => {
        if (result == null)
            return;
        main_1.scheduleArrangerConfig.classId = props.id;
        (0, main_1.scheduleChangePageScreen)(React.createElement(schedule_arranger_page_1.default, { classData: result }));
    });
    return (React.createElement("button", { className: "sa-selector-class-entry tiled-btn", onClick: selectClass },
        React.createElement("span", { className: "sa-selector-class-name" }, props.name),
        React.createElement("span", { className: "sa-selector-class-spec" }, props.specialization)));
};


/***/ }),

/***/ "./React/schedule-arranger/main.tsx":
/*!******************************************!*\
  !*** ./React/schedule-arranger/main.tsx ***!
  \******************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.server = exports.scheduleChangePageScreen = exports.scheduleArrangerConfig = void 0;
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const modals_1 = __webpack_require__(/*! ../shared/modals */ "./React/shared/modals.ts");
const server_connection_1 = __importDefault(__webpack_require__(/*! ../shared/server-connection */ "./React/shared/server-connection.tsx"));
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ./schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
const class_selector_1 = __importDefault(__webpack_require__(/*! ./class-selector */ "./React/schedule-arranger/class-selector.tsx"));
const top_bar_1 = __importDefault(__webpack_require__(/*! ../shared/top-bar */ "./React/shared/top-bar.tsx"));
exports.server = new server_connection_1.default("/ScheduleArranger");
class MainScreen extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.changeScreen = (pageComponent) => {
            this.setState({ pageComponent });
        };
        this._classSelectorComponent =
            react_1.default.createElement(class_selector_1.default, { entries: this.props.classes });
        this.state = {
            pageComponent: this._classSelectorComponent
        };
        exports.scheduleArrangerConfig = this.props.config;
        exports.scheduleChangePageScreen = this.changeScreen;
        schedule_data_service_1.default.classes = this.props.classes;
        schedule_data_service_1.default.subjects = this.props.subjects;
        schedule_data_service_1.default.teachers = this.props.teachers;
        schedule_data_service_1.default.rooms = this.props.rooms;
    }
    componentDidMount() {
        top_bar_1.default.Ref.setGoBackAction(() => this.setState({ pageComponent: this._classSelectorComponent }));
    }
    render() {
        return (react_1.default.createElement("div", { className: "schedule-arranger-main" },
            react_1.default.createElement(top_bar_1.default, null),
            react_1.default.createElement("div", { className: "sa-page-content" }, this.state.pageComponent),
            react_1.default.createElement(modals_1.ModalSpace, null)));
    }
}
exports["default"] = MainScreen;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page.tsx":
/*!************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page.tsx ***!
  \************************************************************/
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
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const React = __importStar(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const selector_1 = __importDefault(__webpack_require__(/*! ./schedule-arranger-page/selector */ "./React/schedule-arranger/schedule-arranger-page/selector.tsx"));
__webpack_require__(/*! ./schedule-arranger-page/schedule-arranger-page.css */ "./React/schedule-arranger/schedule-arranger-page/schedule-arranger-page.css");
const main_1 = __webpack_require__(/*! ./main */ "./React/schedule-arranger/main.tsx");
const schedule_arranger_timeline_1 = __importDefault(__webpack_require__(/*! ./schedule-arranger-page/schedule-arranger-timeline */ "./React/schedule-arranger/schedule-arranger-page/schedule-arranger-timeline.tsx"));
class ScheduleArrangerPage extends React.Component {
    render() {
        return (React.createElement("div", { className: "schedule-arranger-page" },
            React.createElement(selector_1.default, { data: this.props.classData.data }),
            React.createElement(schedule_arranger_timeline_1.default, { config: main_1.scheduleArrangerConfig, data: this.props.classData.data })));
    }
}
exports["default"] = ScheduleArrangerPage;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/schedule-arranger-timeline.tsx":
/*!***************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/schedule-arranger-timeline.tsx ***!
  \***************************************************************************************/
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
const time_column_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/components/time-column */ "./React/schedule-shared/components/time-column.tsx"));
const time_column_variant_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/enums/time-column-variant */ "./React/schedule-shared/enums/time-column-variant.ts"));
const schedule_timeline_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/schedule-timeline */ "./React/schedule-shared/schedule-timeline.tsx"));
const main_1 = __webpack_require__(/*! ../main */ "./React/schedule-arranger/main.tsx");
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
const day_column_1 = __importDefault(__webpack_require__(/*! ./timeline/day-column */ "./React/schedule-arranger/schedule-arranger-page/timeline/day-column.tsx"));
class ScheduleArrangerTimeline extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.dayColumnFactory = (day) => {
            var _a, _b, _c, _d, _e, _f;
            return (react_1.default.createElement(day_column_1.default, { key: day, dayIndicator: day, config: this.props.config, lessons: (_b = (_a = schedule_data_service_1.default.lessons.find(x => x.dayIndicator == day)) === null || _a === void 0 ? void 0 : _a.lessons) !== null && _b !== void 0 ? _b : [], teacherBusyLessons: (_d = (_c = this.state.teacherBusyLessons) === null || _c === void 0 ? void 0 : _c.find(x => x.dayIndicator == day)) === null || _d === void 0 ? void 0 : _d.lessons, roomBusyLessons: (_f = (_e = this.state.roomBusyLessons) === null || _e === void 0 ? void 0 : _e.find(x => x.dayIndicator == day)) === null || _f === void 0 ? void 0 : _f.lessons, addLesson: this.addLesson, editStoredLesson: this.editLesson }));
        };
        this.addLesson = (dayIndicator, cellIndex, time, data) => __awaiter(this, void 0, void 0, function* () {
            this.hideOtherLessonsShadows();
            const prefab = JSON.parse(data.getData("prefab"));
            const lessons = yield schedule_data_service_1.default.getOverlappingLessonsAsync({
                day: dayIndicator,
                time,
                teacherId: prefab === null || prefab === void 0 ? void 0 : prefab.lecturer.id,
                roomId: prefab === null || prefab === void 0 ? void 0 : prefab.room.id
            });
            if (lessons.length)
                return;
            main_1.server.postAsync("Lesson", {}, {
                classId: main_1.scheduleArrangerConfig.classId,
                day: dayIndicator,
                time: time,
                customDuration: undefined,
                subjectId: prefab.subject.id,
                lecturerId: prefab.lecturer.id,
                roomId: prefab.room.id
            }).then(result => {
                if (result.success) {
                    schedule_data_service_1.default.lessons[dayIndicator].lessons.push(result.lesson);
                    this.rerender();
                }
            });
        });
        this.editLesson = (model) => {
            const dayAndLesson = schedule_data_service_1.default.getLessonById(model.id);
            if (!dayAndLesson)
                return;
            if (dayAndLesson.day != model.day) {
                const oldDayLessons = schedule_data_service_1.default.lessons.find(x => x.dayIndicator == dayAndLesson.day);
                oldDayLessons.lessons.splice(oldDayLessons.lessons.indexOf(dayAndLesson.lesson), 1);
                schedule_data_service_1.default.lessons.find(x => x.dayIndicator == model.day).lessons.push(dayAndLesson.lesson);
            }
            dayAndLesson.lesson.time = model.time;
            dayAndLesson.lesson.customDuration = model.customDuration;
            if (dayAndLesson.lesson.lecturer.id != model.lecturerId)
                dayAndLesson.lesson.lecturer = {
                    id: model.lecturerId,
                    name: schedule_data_service_1.default.teachers.find(x => x.id == model.lecturerId).shortName
                };
            if (dayAndLesson.lesson.room.id != model.roomId)
                dayAndLesson.lesson.room = {
                    id: model.roomId,
                    name: schedule_data_service_1.default.rooms.find(x => x.id == model.roomId).name
                };
            if (dayAndLesson.lesson.subject.id != model.subjectId)
                dayAndLesson.lesson.subject = {
                    id: model.subjectId,
                    name: schedule_data_service_1.default.subjects.find(x => x.id == model.subjectId).name
                };
            this.rerender();
        };
        this.initiateShowingOtherLessonsShadows = (event) => __awaiter(this, void 0, void 0, function* () {
            const data = event.detail;
            yield schedule_data_service_1.default.getTeacherAndRoomLessonsAsync(data.lecturer.id, data.room.id, this.displayOtherLessonsShadows);
        });
        this.displayOtherLessonsShadows = (teacher, room) => {
            if (!teacher && !room)
                return;
            this.setState(prevState => {
                let { teacherBusyLessons, roomBusyLessons } = prevState;
                teacherBusyLessons !== null && teacherBusyLessons !== void 0 ? teacherBusyLessons : (teacherBusyLessons = teacher);
                roomBusyLessons !== null && roomBusyLessons !== void 0 ? roomBusyLessons : (roomBusyLessons = room);
                return { teacherBusyLessons, roomBusyLessons };
            });
        };
        this.hideOtherLessonsShadows = () => {
            this.setState({
                teacherBusyLessons: undefined,
                roomBusyLessons: undefined
            });
        };
        this.rerender = () => this.forceUpdate();
        schedule_data_service_1.default.assignDaysFromProps(this.props.data);
        addEventListener('dragBegan', (event) => this.initiateShowingOtherLessonsShadows(event));
        addEventListener('clearOtherLessons', this.hideOtherLessonsShadows);
        addEventListener('timeline-lessons-rerender', this.rerender);
        this.state = {};
    }
    render() {
        return (react_1.default.createElement(schedule_timeline_1.default, { config: this.props.config, className: "schedule-arranger-timeline", dayColumnFactory: this.dayColumnFactory, timeColumn: react_1.default.createElement(time_column_1.default, Object.assign({}, this.props.config, { variant: time_column_variant_1.default.WholeHoursByCellSpec })) }));
    }
}
exports["default"] = ScheduleArrangerTimeline;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/selector.tsx":
/*!*********************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/selector.tsx ***!
  \*********************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const modals_1 = __webpack_require__(/*! ../../shared/modals */ "./React/shared/modals.ts");
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
const add_lesson_prefab_tile_1 = __importDefault(__webpack_require__(/*! ./selector/add-lesson-prefab-tile */ "./React/schedule-arranger/schedule-arranger-page/selector/add-lesson-prefab-tile.tsx"));
const lesson_prefab_mod_comp_1 = __importDefault(__webpack_require__(/*! ./selector/lesson-prefab-mod-comp */ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-mod-comp.tsx"));
const lesson_prefab_tile_1 = __importDefault(__webpack_require__(/*! ./selector/lesson-prefab-tile */ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-tile.tsx"));
class ScheduleArrangerSelector extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.lessonToPrefab = (lesson) => ({
            subject: lesson.subject,
            lecturer: lesson.lecturer,
            room: lesson.room
        });
        this.openAddPrefabModal = () => {
            this._addPrefabModalId = modals_1.modalController.add({
                children: (react_1.default.createElement(lesson_prefab_mod_comp_1.default, { submit: this.addPrefab }))
            });
        };
        this.addPrefab = (info) => {
            modals_1.modalController.closeById(this._addPrefabModalId);
            schedule_data_service_1.default.addPrefab({
                subject: { id: info.subjectId, name: schedule_data_service_1.default.getSubjectName(info.subjectId) },
                lecturer: { id: info.teacherId, name: schedule_data_service_1.default.getTeacherName(info.teacherId) },
                room: { id: info.roomId, name: schedule_data_service_1.default.getRoomName(info.roomId) }
            });
        };
        this.preparePrefabs();
        addEventListener('newPrefab', (_) => this.forceUpdate());
    }
    preparePrefabs() {
        const prefabs = this.props.data.flatMap(dayLessons => dayLessons.lessons).map(this.lessonToPrefab);
        const validPrefabs = [];
        for (let prefab of prefabs) {
            if (validPrefabs.some(x => x.subject.id == prefab.subject.id
                && x.lecturer.id == prefab.lecturer.id
                && x.room.id == prefab.room.id))
                continue;
            validPrefabs.push(prefab);
        }
        schedule_data_service_1.default.prefabs = validPrefabs;
    }
    render() {
        return (react_1.default.createElement("div", { className: "schedule-arranger-selector" },
            schedule_data_service_1.default.prefabs.map((prefab, index) => (react_1.default.createElement(lesson_prefab_tile_1.default, { key: index, data: prefab }))),
            react_1.default.createElement(add_lesson_prefab_tile_1.default, { onClick: this.openAddPrefabModal })));
    }
}
exports["default"] = ScheduleArrangerSelector;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/selector/add-lesson-prefab-tile.tsx":
/*!********************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/selector/add-lesson-prefab-tile.tsx ***!
  \********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const AddLessonPrefabTile = (props) => {
    return (react_1.default.createElement("div", { className: "sa-add-lesson-prefab", onClick: props.onClick },
        react_1.default.createElement("i", { className: "fa-solid fa-plus" })));
};
exports["default"] = AddLessonPrefabTile;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-mod-comp.tsx":
/*!********************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-mod-comp.tsx ***!
  \********************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const form_controls_1 = __webpack_require__(/*! ../../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
class LessonPrefabModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.createOnSelectChangeHandler = (property) => (value) => {
            this.setStateFnData(data => data[property] = value);
        };
        this.changeSubject = value => {
            this.setStateFnData(data => {
                if (value != data.subjectId)
                    data.teacherId = undefined;
                data.subjectId = value;
            });
        };
        this.onSubmit = (e) => {
            e.preventDefault();
            if (!this._validator.validate()) {
                console.log(this._validator.errors);
                this.forceUpdate();
                return;
            }
            this.props.submit(this.state.data);
        };
        this.state = {
            data: {
                subjectId: this.props.subjectId,
                teacherId: this.props.teacherId,
                roomId: this.props.roomId
            }
        };
        this._validator.setRules({
            subjectId: { notNull: true },
            teacherId: { notNull: true },
            roomId: { notNull: true }
        });
    }
    render() {
        return (react_1.default.createElement("form", { onSubmit: this.onSubmit },
            react_1.default.createElement(form_controls_1.Select, { label: "Przedmiot", name: "subject-input", value: this.state.data.subjectId, onChangeId: this.changeSubject, errorMessages: this._validator.errors.filter(x => x.on == 'subjectId').map(x => x.error), options: schedule_data_service_1.default.subjects.map(x => ({
                    label: x.name,
                    value: x.id
                })) }),
            react_1.default.createElement(form_controls_1.Select, { label: "Nauczyciel", name: "teacher-input", value: this.state.data.teacherId, onChangeId: this.createOnSelectChangeHandler('teacherId'), errorMessages: this._validator.errors.filter(x => x.on == 'teacherId').map(x => x.error), options: schedule_data_service_1.default.getTeachersBySubject(this.state.data.subjectId).map(x => ({
                    label: x.name,
                    value: x.id
                })) }),
            react_1.default.createElement(form_controls_1.Select, { label: "Pomieszczenie", name: "room-input", value: this.state.data.roomId, onChangeId: this.createOnSelectChangeHandler('roomId'), errorMessages: this._validator.errors.filter(x => x.on == 'roomId').map(x => x.error), options: schedule_data_service_1.default.rooms.map(x => ({
                    label: x.name,
                    value: x.id
                })) }),
            react_1.default.createElement(form_controls_1.SubmitButton, { value: "Zapisz" })));
    }
}
exports["default"] = LessonPrefabModComp;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-tile.tsx":
/*!****************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab-tile.tsx ***!
  \****************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
__webpack_require__(/*! ./lesson-prefab.css */ "./React/schedule-arranger/schedule-arranger-page/selector/lesson-prefab.css");
class LessonPrefabTile extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.onStart = (event) => {
            event.dataTransfer.setData("prefab", JSON.stringify(this.props.data));
            schedule_data_service_1.default.isTileDragged = true;
            dispatchEvent(new CustomEvent('dragBegan', {
                detail: this.props.data
            }));
        };
        this.onEnd = (event) => {
            schedule_data_service_1.default.isTileDragged = false;
            dispatchEvent(new Event("hideLessonShadow"));
            dispatchEvent(new Event("clearOtherLessons"));
        };
    }
    render() {
        var _a;
        return (react_1.default.createElement("div", { className: "sa-lesson-prefab", draggable: true, onDragStart: this.onStart, onDragEnd: this.onEnd },
            react_1.default.createElement("span", { className: "sa-lesson-prefab-subject" }, this.props.data.subject.name),
            react_1.default.createElement("div", { className: "sa-lesson-prefab-bottom" },
                react_1.default.createElement("div", { className: "sa-lesson-prefab-lecturer" }, this.props.data.lecturer.name),
                react_1.default.createElement("div", { className: "sa-lesson-prefab-room" }, (_a = this.props.data.room) === null || _a === void 0 ? void 0 : _a.name))));
    }
}
exports["default"] = LessonPrefabTile;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/day-column.tsx":
/*!********************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/day-column.tsx ***!
  \********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const day_column_base_1 = __importDefault(__webpack_require__(/*! ../../../schedule-shared/components/day-column-base */ "./React/schedule-shared/components/day-column-base.tsx"));
const lesson_placing_shadow_1 = __importDefault(__webpack_require__(/*! ./lesson-placing-shadow */ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-placing-shadow.tsx"));
const lessons_by_day_1 = __importDefault(__webpack_require__(/*! ./lessons-by-day */ "./React/schedule-arranger/schedule-arranger-page/timeline/lessons-by-day.tsx"));
const room_busy_lessons_1 = __importDefault(__webpack_require__(/*! ./room-busy-lessons */ "./React/schedule-arranger/schedule-arranger-page/timeline/room-busy-lessons.tsx"));
const teacher_busy_lessons_1 = __importDefault(__webpack_require__(/*! ./teacher-busy-lessons */ "./React/schedule-arranger/schedule-arranger-page/timeline/teacher-busy-lessons.tsx"));
const timeline_cell_1 = __importDefault(__webpack_require__(/*! ./timeline-cell */ "./React/schedule-arranger/schedule-arranger-page/timeline/timeline-cell.tsx"));
class DayColumn extends day_column_base_1.default {
    constructor(props) {
        super(props);
        this._iAmCallingHideShadow = false;
        this.addLesson = (dayIndicator, cellIndex, time, data) => {
            this.hideLessonShadow();
            this.props.addLesson(dayIndicator, cellIndex, time, data);
        };
        this.onEntered = (dayIndicator, cellIndex, time) => {
            this._iAmCallingHideShadow = true;
            dispatchEvent(new Event('hideLessonShadow'));
            this._iAmCallingHideShadow = false;
            this.setState({ shadowFor: time });
        };
        this.hideLessonShadow = () => {
            if (this.state.shadowFor && !this._iAmCallingHideShadow)
                this.setState({ shadowFor: undefined });
        };
        addEventListener('hideLessonShadow', () => this.hideLessonShadow());
        this.instantiateCells();
    }
    instantiateCells() {
        if (!this.getTimelineCellComponent)
            throw new Error("Overriding method `getTimelineCellComponent` is required for calling `instantiateCells`");
        const cellsPerHour = 60 / this.props.config.cellDuration;
        const count = (this.props.config.endHour - this.props.config.startHour) * cellsPerHour;
        const cellTimes = Array.from({ length: count }, (_, i) => {
            const minutesFromMidnight = (this.props.config.startHour * 60) + this.props.config.cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        });
        this._cells = cellTimes.map((cellTime, i) => this.getTimelineCellComponent(cellTime, i));
    }
    getContainerProps() {
        return {
            onDragEnd: this.hideLessonShadow
        };
    }
    getLessonsDisplayComponent() {
        return (react_1.default.createElement(lessons_by_day_1.default, { lessons: this.props.lessons, day: this.props.dayIndicator, editStoredLesson: this.props.editStoredLesson }));
    }
    getTimelineCellComponent(time, index) {
        return (react_1.default.createElement(timeline_cell_1.default, { key: index, height: this.props.config.cellHeight, dayIndicator: this.props.dayIndicator, cellIndex: index, dropped: this.props.addLesson, entered: this.onEntered, time: time }));
    }
    getAdditionalComponents() {
        return [
            react_1.default.createElement(room_busy_lessons_1.default, { lessons: this.props.roomBusyLessons, key: "rooms" }),
            react_1.default.createElement(teacher_busy_lessons_1.default, { lessons: this.props.teacherBusyLessons, key: "teachers" }),
            react_1.default.createElement(lesson_placing_shadow_1.default, { time: this.state.shadowFor, key: "thisClass" })
        ];
    }
}
exports["default"] = DayColumn;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx":
/*!*****************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx ***!
  \*****************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const main_1 = __webpack_require__(/*! ../../main */ "./React/schedule-arranger/main.tsx");
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
__webpack_require__(/*! ./lesson-tiles.css */ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-tiles.css");
__webpack_require__(/*! ./other-lesson-tiles.css */ "./React/schedule-arranger/schedule-arranger-page/timeline/other-lesson-tiles.css");
class GenericLessonTile extends react_1.default.Component {
    calcTopOffset() {
        const minutes = (this.props.time.hour - main_1.scheduleArrangerConfig.startHour) * 60 + this.props.time.minutes;
        const cells = minutes / main_1.scheduleArrangerConfig.cellDuration;
        return cells * main_1.scheduleArrangerConfig.cellHeight;
    }
    calcHeight() {
        var _a;
        const duration = (_a = this.props.customDuration) !== null && _a !== void 0 ? _a : main_1.scheduleArrangerConfig.defaultLessonDuration;
        const cells = duration / main_1.scheduleArrangerConfig.cellDuration;
        return cells * main_1.scheduleArrangerConfig.cellHeight;
    }
    render() {
        let style = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        };
        return (react_1.default.createElement("button", { className: `sa-lesson-tile ${schedule_data_service_1.default.isTileDragged ? 'sa-lesson-tile-behind' : ''} ${this.props.className}`, style: style, onClick: this.props.onPress }, this.props.children));
    }
}
exports["default"] = GenericLessonTile;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.tsx":
/*!*****************************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.tsx ***!
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
const day_of_week_1 = __importDefault(__webpack_require__(/*! ../../../../schedule-shared/enums/day-of-week */ "./React/schedule-shared/enums/day-of-week.ts"));
const time_functions_1 = __webpack_require__(/*! ../../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const weekdays_function_1 = __webpack_require__(/*! ../../../../schedule-shared/help/weekdays-function */ "./React/schedule-shared/help/weekdays-function.ts");
const enum_help_1 = __webpack_require__(/*! ../../../../shared/enum-help */ "./React/shared/enum-help.ts");
const form_controls_1 = __webpack_require__(/*! ../../../../shared/form-controls */ "./React/shared/form-controls.ts");
const mod_comp_base_1 = __importDefault(__webpack_require__(/*! ../../../../shared/form-controls/mod-comp-base */ "./React/shared/form-controls/mod-comp-base.tsx"));
const main_1 = __webpack_require__(/*! ../../../main */ "./React/schedule-arranger/main.tsx");
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../../../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
__webpack_require__(/*! ./lesson-mod-comp.css */ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.css");
const overlapping_lesson_pad_1 = __importDefault(__webpack_require__(/*! ./overlapping-lesson-pad */ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/overlapping-lesson-pad.tsx"));
class LessonModComp extends mod_comp_base_1.default {
    constructor(props) {
        super(props);
        this.changeTimeAsync = (event) => __awaiter(this, void 0, void 0, function* () {
            const value = event.target.value;
            yield this.refreshAsync(state => state.data.time = this.timeFromInput(value));
        });
        this.changeCustomDuration = (event) => __awaiter(this, void 0, void 0, function* () {
            const value = event.target.value;
            yield this.refreshAsync(state => state.data.customDuration = parseInt(value));
        });
        this.changeUseDefaultDuration = (event) => __awaiter(this, void 0, void 0, function* () {
            const value = event.target.checked;
            yield this.refreshAsync(state => state.defaultDuration = value);
        });
        this.changeDay = (value) => __awaiter(this, void 0, void 0, function* () {
            if (value instanceof Array)
                return;
            if (this.validateDay(value))
                yield this.refreshAsync(state => state.data.day = value);
        });
        this.changeSubject = (value) => __awaiter(this, void 0, void 0, function* () {
            yield this.refreshAsync(state => {
                if (value != state.data.subjectId)
                    state.data.lecturerId = undefined;
                state.data.subjectId = value;
            });
        });
        this.changeLecturer = (value) => __awaiter(this, void 0, void 0, function* () {
            if (value instanceof Array)
                value = value[0];
            const id = value.value;
            const isMainTeacher = value.isMainTeacher;
            yield this.refreshAsync(state => state.data.lecturerId = id, () => {
                if (!isMainTeacher)
                    this._validator.addWarning('lecturerId', "Wybrany nauczyciel należy do dodatkowych z danego przedmiotu");
            });
        });
        this.changeRoom = (value) => __awaiter(this, void 0, void 0, function* () {
            yield this.refreshAsync(state => state.data.roomId = value);
        });
        this.submitAsync = (e) => __awaiter(this, void 0, void 0, function* () {
            e.preventDefault();
            if (!this._validator.validate()) {
                this.forceUpdate();
                return;
            }
            const response = yield main_1.server.postAsync("LessonModification", {}, this.state.data);
            if (response.success) {
                this.props.editStoredLesson(this.state.data);
                this.props.assignedAtPresenter.close();
            }
        });
        this.removeLesson = () => {
            schedule_data_service_1.default.removeLessonAndGetResultAsync(this.state.data.id)
                .then(result => {
                if (result)
                    this.props.assignedAtPresenter.close();
            });
        };
        this.state = {
            data: {
                id: this.props.lesson.id,
                day: this.props.day,
                time: this.props.lesson.time,
                customDuration: this.props.lesson.customDuration,
                subjectId: this.props.lesson.subject.id,
                lecturerId: this.props.lesson.lecturer.id,
                roomId: this.props.lesson.room.id,
                classId: main_1.scheduleArrangerConfig.classId
            },
            defaultDuration: this.props.lesson.customDuration == undefined
        };
        this._validator.setRules({
            day: { notNull: true },
            time: {
                notNull: true, other: (model, prop) => {
                    return this.validateTime(model[prop])
                        ? undefined
                        : {
                            error: `Termin nie mieści się w ustalonym zakresie (${main_1.scheduleArrangerConfig.startHour}:00 - ${main_1.scheduleArrangerConfig.endHour}:00)`,
                            on: prop
                        };
                }
            },
            subjectId: { notNull: true },
            lecturerId: { notNull: true },
            roomId: { notNull: true }
        });
        this._dayOptions = (0, enum_help_1.getEnumValues)(day_of_week_1.default).map(x => ({
            value: x,
            label: (0, weekdays_function_1.nameForDayOfWeek)(x)
        }));
    }
    get _errorMessage() {
        var _a;
        return ((_a = this.state.overlappingLessons) === null || _a === void 0 ? void 0 : _a.length)
            ? "Zajęcia kolidują z innymi"
            : this._validator.errors.length
                ? "Błędy w formularzu"
                : undefined;
    }
    render() {
        var _a, _b, _c;
        return (react_1.default.createElement("form", { onSubmit: this.submitAsync },
            react_1.default.createElement("div", { className: "lesson-mod-comp-layout" },
                react_1.default.createElement("div", { className: "lmc-inputs" },
                    react_1.default.createElement(form_controls_1.Input, { label: "Godzina rozpocz\u0119cia", name: "time-input", value: this.timeToInput(this.state.data.time), errorMessages: this._validator.getErrorMsgsFor('time'), onChange: this.changeTimeAsync, type: "time" }),
                    react_1.default.createElement(form_controls_1.Select, { label: "Dzie\u0144", name: "day-input", value: this.state.data.day, errorMessages: this._validator.getErrorMsgsFor('day'), onChangeId: this.changeDay, options: this._dayOptions }),
                    react_1.default.createElement(form_controls_1.Input, { label: "Czas trwania", name: "duration-input", value: (_a = this.state.data.customDuration) !== null && _a !== void 0 ? _a : main_1.scheduleArrangerConfig.defaultLessonDuration, errorMessages: this._validator.getErrorMsgsFor('customDuration'), onChange: this.changeCustomDuration, disabled: this.state.defaultDuration, type: "number" }),
                    react_1.default.createElement(form_controls_1.Input, { inputClassName: "form-check-input", label: `Domyślny czas trwania (${main_1.scheduleArrangerConfig.defaultLessonDuration} minut)`, name: "default-duration-input", checked: this.state.defaultDuration, onChange: this.changeUseDefaultDuration, type: "checkbox" }),
                    react_1.default.createElement(form_controls_1.Select, { label: "Przedmiot", name: "subject-input", value: this.state.data.subjectId, onChangeId: this.changeSubject, options: schedule_data_service_1.default.subjects.map(x => ({
                            label: x.name,
                            value: x.id
                        })), errorMessages: this._validator.getErrorMsgsFor('subjectId') }),
                    react_1.default.createElement(form_controls_1.Select, { label: "Nauczyciel", name: "lecturer-input", value: this.state.data.lecturerId, onChange: this.changeLecturer, options: schedule_data_service_1.default.getTeachersBySubject(this.state.data.subjectId).map(x => ({
                            label: x.name,
                            value: x.id,
                            isMainTeacher: x.isMainTeacher
                        })), errorMessages: this._validator.getErrorMsgsFor('lecturerId'), warningMessages: this._validator.getWarningMsgsFor('lecturerId'), optionStyle: (props) => ({
                            backgroundColor: props.isSelected
                                ? '#c9fbff'
                                : props.data.isMainTeacher
                                    ? '#ffffff'
                                    : '#fffa62',
                            color: '#000000'
                        }) }),
                    react_1.default.createElement(form_controls_1.Select, { label: "Pomieszczenie", name: "room-input", value: this.state.data.roomId, onChangeId: this.changeRoom, options: schedule_data_service_1.default.rooms.map(x => ({
                            label: x.name,
                            value: x.id
                        })), errorMessages: this._validator.getErrorMsgsFor('roomId') })),
                react_1.default.createElement("div", { className: "lmc-right-panel" },
                    react_1.default.createElement("h4", null, "Koliduj\u0105ce zaj\u0119cia"),
                    react_1.default.createElement("div", { className: "lmc-overlap-container" }, (_b = this.state.overlappingLessons) === null || _b === void 0 ? void 0 : _b.map(lesson => (react_1.default.createElement(overlapping_lesson_pad_1.default, { key: lesson.id, lesson: lesson, refreshAsync: () => this.refreshAsync() })))),
                    react_1.default.createElement("div", { className: "lmc-right-bottom" },
                        react_1.default.createElement("button", { className: "lmc-save-btn", disabled: this._errorMessage != undefined }, (_c = this._errorMessage) !== null && _c !== void 0 ? _c : "Zapisz"),
                        react_1.default.createElement("button", { type: "button", className: "lmc-remove-btn", onClick: this.removeLesson }, "Usu\u0144 zaj\u0119cia"))))));
    }
    validateTime(time) {
        const minutes = time.hour * 60 + time.minutes;
        return !(time.hour < main_1.scheduleArrangerConfig.startHour
            || minutes > main_1.scheduleArrangerConfig.endHour * 60);
    }
    validateDay(day) {
        if (!(0, enum_help_1.getEnumValues)(day_of_week_1.default).includes(day))
            return false;
        return true;
    }
    timeFromInput(display) {
        const numbers = display.split(':').map(x => parseInt(x));
        if (numbers.some(x => isNaN(x)))
            return { hour: 0, minutes: 0 };
        return {
            hour: numbers[0],
            minutes: numbers[1]
        };
    }
    timeToInput(time) {
        return `${(0, time_functions_1.displayMinutes)(time.hour)}:${(0, time_functions_1.displayMinutes)(time.minutes)}`;
    }
    refreshAsync(modifyStateMethod, beforeRerender) {
        return __awaiter(this, void 0, void 0, function* () {
            const newState = Object.assign({}, this.state);
            modifyStateMethod === null || modifyStateMethod === void 0 ? void 0 : modifyStateMethod(newState);
            let lessons;
            if (this._validator.validate()) {
                lessons = yield schedule_data_service_1.default.getOverlappingLessonsAsync({
                    day: newState.data.day,
                    time: newState.data.time,
                    customDuration: newState.defaultDuration ? undefined : newState.data.customDuration,
                    teacherId: newState.data.lecturerId,
                    roomId: newState.data.roomId
                }, this.state.data.id);
            }
            else
                lessons = [];
            beforeRerender === null || beforeRerender === void 0 ? void 0 : beforeRerender();
            this.setStateFn(state => state.overlappingLessons = lessons, modifyStateMethod);
        });
    }
}
exports["default"] = LessonModComp;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/overlapping-lesson-pad.tsx":
/*!************************************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/overlapping-lesson-pad.tsx ***!
  \************************************************************************************************************/
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
const time_functions_1 = __webpack_require__(/*! ../../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const schedule_data_service_1 = __importDefault(__webpack_require__(/*! ../../../schedule-data-service */ "./React/schedule-arranger/schedule-data-service.ts"));
class OverlappingLessonPad extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.confirmAndRemoveAsync = () => __awaiter(this, void 0, void 0, function* () {
            if (yield schedule_data_service_1.default.removeLessonAndGetResultAsync(this.props.lesson.id))
                yield this.props.refreshAsync();
        });
    }
    render() {
        return (react_1.default.createElement("div", { className: "lmc-overlap-pad" },
            react_1.default.createElement("div", { className: "lmc-op-class" }, this.props.lesson.orgClass.name),
            react_1.default.createElement("div", { className: "lmc-op-subject" }, this.props.lesson.subject.name),
            react_1.default.createElement("div", { className: "lmc-op-time" }, (0, time_functions_1.displayTime)(this.props.lesson.time)),
            react_1.default.createElement("div", { className: "lmc-op-lecturer" }, this.props.lesson.lecturer.name),
            react_1.default.createElement("div", { className: "lmc-op-room" }, this.props.lesson.room.name),
            react_1.default.createElement("div", { className: "btn-group dropend lmc-op-more" },
                react_1.default.createElement("button", { type: "button", className: "lmc-op-more-btn", "data-bs-toggle": "dropdown", "aria-expanded": "false" },
                    react_1.default.createElement("i", { className: "fa-solid fa-ellipsis-vertical" })),
                react_1.default.createElement("ul", { className: "dropdown-menu" },
                    react_1.default.createElement("li", null,
                        react_1.default.createElement("a", { className: "dropdown-item", onClick: this.confirmAndRemoveAsync }, "Usu\u0144 zaj\u0119cia")),
                    react_1.default.createElement("li", null,
                        react_1.default.createElement("a", { className: "dropdown-item", href: "#" },
                            "Przejd\u017A do klasy ",
                            this.props.lesson.orgClass.name))))));
    }
}
exports["default"] = OverlappingLessonPad;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-placing-shadow.tsx":
/*!*******************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lesson-placing-shadow.tsx ***!
  \*******************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_functions_1 = __webpack_require__(/*! ../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const main_1 = __webpack_require__(/*! ../../main */ "./React/schedule-arranger/main.tsx");
const generic_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./generic-lesson-tile */ "./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx"));
class LessonPlacingShadow extends react_1.default.Component {
    render() {
        if (!this.props.time)
            return react_1.default.createElement(react_1.default.Fragment, null);
        return (react_1.default.createElement(generic_lesson_tile_1.default, { className: "sa-lesson-placing-shadow", time: this.props.time },
            react_1.default.createElement("h4", null,
                (0, time_functions_1.displayTime)(this.props.time),
                " - ",
                (0, time_functions_1.displayTime)((0, time_functions_1.sumTimes)(this.props.time, { hour: 0, minutes: main_1.scheduleArrangerConfig.defaultLessonDuration })))));
    }
}
exports["default"] = LessonPlacingShadow;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/lessons-by-day.tsx":
/*!************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/lessons-by-day.tsx ***!
  \************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_functions_1 = __webpack_require__(/*! ../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const modals_1 = __webpack_require__(/*! ../../../shared/modals */ "./React/shared/modals.ts");
const generic_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./generic-lesson-tile */ "./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx"));
const lesson_mod_comp_1 = __importDefault(__webpack_require__(/*! ./lesson-mod-comp/lesson-mod-comp */ "./React/schedule-arranger/schedule-arranger-page/timeline/lesson-mod-comp/lesson-mod-comp.tsx"));
class LessonsByDay extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.openModificationComponent = (lesson) => {
            modals_1.modalController.addCustomComponent({
                modificationComponent: lesson_mod_comp_1.default,
                modificationComponentProps: {
                    lesson,
                    day: this.props.day,
                    editStoredLesson: this.props.editStoredLesson
                },
                style: {
                    width: '800px'
                }
            });
        };
    }
    render() {
        return (react_1.default.createElement(react_1.default.Fragment, null, this.props.lessons.map(lesson => {
            var _a;
            return react_1.default.createElement(generic_lesson_tile_1.default, { className: "sa-placed-lesson", key: `${lesson.time.hour}${lesson.time.minutes}`, time: lesson.time, customDuration: lesson.customDuration, onPress: () => this.openModificationComponent(lesson) },
                react_1.default.createElement("div", { className: "sa-lesson-time" }, (0, time_functions_1.displayTime)(lesson.time)),
                react_1.default.createElement("div", { className: "sa-lesson-subject" }, lesson.subject.name),
                react_1.default.createElement("div", { className: "sa-lesson-lecturer" }, lesson.lecturer.name),
                react_1.default.createElement("div", { className: "sa-lesson-room" }, (_a = lesson.room) === null || _a === void 0 ? void 0 : _a.name));
        })));
    }
}
exports["default"] = LessonsByDay;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/room-busy-lessons.tsx":
/*!***************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/room-busy-lessons.tsx ***!
  \***************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const generic_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./generic-lesson-tile */ "./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx"));
const RoomBusyLessons = (props) => {
    if (!props.lessons)
        return react_1.default.createElement(react_1.default.Fragment, null);
    return (react_1.default.createElement(react_1.default.Fragment, null, props.lessons.map(x => (react_1.default.createElement(generic_lesson_tile_1.default, { className: "sa-lessons-room-busy", key: x.id, time: x.time },
        react_1.default.createElement("div", null, x.room.name))))));
};
exports["default"] = RoomBusyLessons;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/teacher-busy-lessons.tsx":
/*!******************************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/teacher-busy-lessons.tsx ***!
  \******************************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const generic_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./generic-lesson-tile */ "./React/schedule-arranger/schedule-arranger-page/timeline/generic-lesson-tile.tsx"));
const TeacherBusyLessons = (props) => {
    if (!props.lessons)
        return react_1.default.createElement(react_1.default.Fragment, null);
    return (react_1.default.createElement(react_1.default.Fragment, null, props.lessons.map(x => (react_1.default.createElement(generic_lesson_tile_1.default, { className: "sa-lessons-teacher-busy", key: x.id, time: x.time },
        react_1.default.createElement("div", null, x.lecturer.name))))));
};
exports["default"] = TeacherBusyLessons;


/***/ }),

/***/ "./React/schedule-arranger/schedule-arranger-page/timeline/timeline-cell.tsx":
/*!***********************************************************************************!*\
  !*** ./React/schedule-arranger/schedule-arranger-page/timeline/timeline-cell.tsx ***!
  \***********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const timeline_cell_base_1 = __importDefault(__webpack_require__(/*! ../../../schedule-shared/components/day-column/timeline-cell-base */ "./React/schedule-shared/components/day-column/timeline-cell-base.tsx"));
class TimelineCell extends timeline_cell_base_1.default {
    constructor() {
        super(...arguments);
        this.onDrop = (event) => {
            this.props.dropped(this.props.dayIndicator, this.props.cellIndex, this.props.time, event.dataTransfer);
        };
        this.onDragOver = (event) => {
            event.preventDefault();
        };
        this.onDragEnter = (_) => {
            this.props.entered(this.props.dayIndicator, this.props.cellIndex, this.props.time);
        };
    }
    getContainerProps() {
        return {
            onDrop: this.onDrop,
            onDragOver: this.onDragOver,
            onDragEnter: this.onDragEnter
        };
    }
}
exports["default"] = TimelineCell;


/***/ }),

/***/ "./React/schedule-arranger/schedule-data-service.ts":
/*!**********************************************************!*\
  !*** ./React/schedule-arranger/schedule-data-service.ts ***!
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
const day_of_week_1 = __importDefault(__webpack_require__(/*! ../schedule-shared/enums/day-of-week */ "./React/schedule-shared/enums/day-of-week.ts"));
const time_functions_1 = __webpack_require__(/*! ../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const modals_1 = __webpack_require__(/*! ../shared/modals */ "./React/shared/modals.ts");
const main_1 = __webpack_require__(/*! ./main */ "./React/schedule-arranger/main.tsx");
const teachers_by_subject_svc_1 = __importDefault(__webpack_require__(/*! ./services/teachers-by-subject-svc */ "./React/schedule-arranger/services/teachers-by-subject-svc.ts"));
class ScheduleArrangerDataService {
    constructor() {
        this.prefabs = [];
        this.getSubjectName = (id) => this.subjects.find(x => x.id == id).name;
        this.getTeacherName = (id) => this.teachers.find(x => x.id == id).shortName;
        this.getRoomName = (id) => this.rooms.find(x => x.id == id).name;
        this.isTileDragged = false;
        this.getTeacherAndRoomLessonsAsync = (teacherId, roomId, apply) => __awaiter(this, void 0, void 0, function* () {
            const teacher = this.teachers.find(x => x.id == teacherId);
            const room = this.rooms.find(x => x.id == roomId);
            if (!teacher || !room)
                return;
            apply(teacher.lessons, room.lessons);
            if (yield this.fetchFromServerAsync(teacher, room)) {
                apply(teacher.lessons, room.lessons);
            }
        });
    }
    addPrefab(prefab) {
        this.prefabs.push(prefab);
        dispatchEvent(new CustomEvent('newPrefab', {
            detail: prefab
        }));
    }
    assignDaysFromProps(days) {
        var _a;
        this.lessons = [];
        for (const dayOfWeekIt in day_of_week_1.default) {
            if (isNaN(dayOfWeekIt))
                continue;
            const dayOfWeek = dayOfWeekIt;
            this.lessons.push((_a = days.find(x => x.dayIndicator == dayOfWeek)) !== null && _a !== void 0 ? _a : { dayIndicator: dayOfWeek, lessons: [] });
        }
    }
    getOverlappingLessonsAsync(checkFor, exceptId) {
        var _a, _b, _c, _d;
        return __awaiter(this, void 0, void 0, function* () {
            const teacher = this.teachers.find(x => x.id == checkFor.teacherId);
            const room = this.rooms.find(x => x.id == checkFor.roomId);
            yield this.fetchFromServerAsync(teacher, room);
            const selectedClass = this.classes.find(x => x.id == main_1.scheduleArrangerConfig.classId);
            const lessons = this.lessons.find(x => x.dayIndicator == checkFor.day)
                .lessons.map(x => (Object.assign(Object.assign({}, x), { orgClass: { id: selectedClass.id, name: selectedClass.name } })))
                .concat((_b = (_a = teacher === null || teacher === void 0 ? void 0 : teacher.lessons.find(x => x.dayIndicator == checkFor.day)) === null || _a === void 0 ? void 0 : _a.lessons) !== null && _b !== void 0 ? _b : [])
                .concat((_d = (_c = room === null || room === void 0 ? void 0 : room.lessons.find(x => x.dayIndicator == checkFor.day)) === null || _c === void 0 ? void 0 : _c.lessons) !== null && _d !== void 0 ? _d : [])
                .filter(x => x.id != exceptId);
            return lessons.filter(x => {
                var _a, _b;
                return (0, time_functions_1.areTimesOverlappingByDuration)(checkFor.time, (_a = checkFor.customDuration) !== null && _a !== void 0 ? _a : main_1.scheduleArrangerConfig.defaultLessonDuration, x.time, (_b = x.customDuration) !== null && _b !== void 0 ? _b : main_1.scheduleArrangerConfig.defaultLessonDuration);
            });
        });
    }
    getTeachersBySubject(subjectId) {
        const svc = new teachers_by_subject_svc_1.default(this.teachers);
        return svc.getFor(subjectId);
    }
    getLessonById(id) {
        for (const lessons of this.lessons) {
            const lesson = lessons.lessons.find(x => x.id == id);
            if (lesson)
                return { day: lessons.dayIndicator, lesson };
        }
        return undefined;
    }
    removeLessonAndGetResultAsync(id) {
        return __awaiter(this, void 0, void 0, function* () {
            const dayAndLesson = this.getLessonById(id);
            if (!dayAndLesson)
                return true;
            if (!(yield new Promise(resolve => {
                modals_1.modalController.addConfirmation({
                    header: "Usuwanie zajęć",
                    text: `Czy na pewno chcesz usunąć zajęcia z przedmiotu '${dayAndLesson.lesson.subject.name}'?`,
                    onConfirm: () => resolve(true),
                    onDecline: () => resolve(false)
                });
            })))
                return false;
            const response = yield main_1.server.postAsync("DeleteLesson", {
                id: dayAndLesson.lesson.id
            });
            if (response.success) {
                this.lessons[dayAndLesson.day].lessons.splice(this.lessons[dayAndLesson.day].lessons.indexOf(dayAndLesson.lesson), 1);
                dispatchEvent(new Event("timeline-lessons-rerender"));
            }
            else
                console.log(response.message);
            return response.success;
        });
    }
    fetchFromServerAsync(teacher, room) {
        var _a, _b;
        return __awaiter(this, void 0, void 0, function* () {
            const fetchTeacher = teacher != undefined && (teacher === null || teacher === void 0 ? void 0 : teacher.lessons) == undefined, fetchRoom = room != undefined && (room === null || room === void 0 ? void 0 : room.lessons) == undefined;
            if (!fetchTeacher && !fetchRoom)
                return false;
            var response = yield main_1.server.getAsync("OtherLessons", {
                classId: main_1.scheduleArrangerConfig.classId,
                teacherId: fetchTeacher ? teacher === null || teacher === void 0 ? void 0 : teacher.id : undefined,
                roomId: fetchRoom ? room === null || room === void 0 ? void 0 : room.id : undefined
            });
            if (response) {
                (_a = teacher.lessons) !== null && _a !== void 0 ? _a : (teacher.lessons = response.teacher);
                (_b = room.lessons) !== null && _b !== void 0 ? _b : (room.lessons = response.room);
            }
            return true;
        });
    }
}
const dataService = new ScheduleArrangerDataService;
exports["default"] = dataService;


/***/ }),

/***/ "./React/schedule-arranger/services/teachers-by-subject-svc.ts":
/*!*********************************************************************!*\
  !*** ./React/schedule-arranger/services/teachers-by-subject-svc.ts ***!
  \*********************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
class TeachersBySubjectSvc {
    constructor(_teachers) {
        this._teachers = _teachers;
    }
    getFor(subjectId) {
        return [
            ...this._teachers
                .filter(x => x.mainSubjectIds.includes(subjectId))
                .map(x => ({ id: x.id, name: x.name, isMainTeacher: true })),
            ...this._teachers
                .filter(x => !x.mainSubjectIds.includes(subjectId) && x.additionalSubjectIds.includes(subjectId))
                .map(x => ({ id: x.id, name: x.name, isMainTeacher: false }))
        ];
    }
}
exports["default"] = TeachersBySubjectSvc;


/***/ }),

/***/ "./React/schedule-shared/components/time-column.tsx":
/*!**********************************************************!*\
  !*** ./React/schedule-shared/components/time-column.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_column_variant_1 = __importDefault(__webpack_require__(/*! ../enums/time-column-variant */ "./React/schedule-shared/enums/time-column-variant.ts"));
const time_functions_1 = __webpack_require__(/*! ../help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
__webpack_require__(/*! ./time-column.css */ "./React/schedule-shared/components/time-column.css");
class TimeColumn extends react_1.default.Component {
    constructor() {
        super(...arguments);
        this.timeLabel = (time, top) => (react_1.default.createElement("div", { className: "sched-time-label my-text-shadow", key: `${time.hour}${time.minutes}`, style: { top } }, (0, time_functions_1.displayTime)(time)));
    }
    render() {
        this._timeLables = [];
        this.addTimeLabelsByVariant();
        return (react_1.default.createElement("div", { className: "sched-time-column" }, this._timeLables));
    }
    addTimeLabelsByVariant() {
        var _a;
        switch ((_a = this.props.variant) !== null && _a !== void 0 ? _a : time_column_variant_1.default.WholeHoursByCellSpec) {
            case time_column_variant_1.default.WholeHoursByCellSpec:
                this.addWholeHoursByConfig();
                break;
            case time_column_variant_1.default.WholeHoursByHeight:
                this.addWholeHoursByHeight();
                break;
        }
    }
    addWholeHoursByConfig() {
        const offsetIncrement = (60 / this.props.cellDuration) * this.props.cellHeight;
        this.addWholeHours(offsetIncrement);
    }
    addWholeHoursByHeight() {
        var _a;
        const hours = this._wholeHoursInRangeFromProps;
        if (!hours.length)
            return;
        const offsetIncrement = ((_a = this.props.scheduleHeight) !== null && _a !== void 0 ? _a : 0) / hours.length;
        this.addWholeHours(offsetIncrement);
    }
    addWholeHours(incrementTopOffsetForEachHour) {
        const hours = this._wholeHoursInRangeFromProps;
        if (!hours.length)
            return;
        let offset = 0;
        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += incrementTopOffsetForEachHour;
        }
    }
    get _wholeHoursInRangeFromProps() {
        return Array.from({
            length: this.props.endHour - this.props.startHour
        }, (_, i) => this.props.startHour + i);
    }
}
exports["default"] = TimeColumn;


/***/ }),

/***/ "./React/schedule-shared/enums/time-column-variant.ts":
/*!************************************************************!*\
  !*** ./React/schedule-shared/enums/time-column-variant.ts ***!
  \************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
var TimeColumnVariant;
(function (TimeColumnVariant) {
    TimeColumnVariant[TimeColumnVariant["WholeHoursByCellSpec"] = 0] = "WholeHoursByCellSpec";
    TimeColumnVariant[TimeColumnVariant["WholeHoursByHeight"] = 1] = "WholeHoursByHeight";
})(TimeColumnVariant || (TimeColumnVariant = {}));
exports["default"] = TimeColumnVariant;


/***/ }),

/***/ "./React/schedule-shared/help/time-functions.ts":
/*!******************************************************!*\
  !*** ./React/schedule-shared/help/time-functions.ts ***!
  \******************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.displayMinutes = exports.displayTime = exports.sumTimes = exports.toMinutes = exports.areTimesOverlappingByDuration = void 0;
const areTimesOverlappingByDuration = (timeAStart, durationA, timeBStart, durationB) => {
    const aStart = (0, exports.toMinutes)(timeAStart);
    const aEnd = aStart + durationA;
    const bStart = (0, exports.toMinutes)(timeBStart);
    const bEnd = bStart + durationB;
    const left = aStart > bStart && aStart < bEnd;
    const right = aEnd > bStart && aEnd < bEnd;
    const over = aStart <= bStart && aEnd >= bEnd;
    return left || right || over;
};
exports.areTimesOverlappingByDuration = areTimesOverlappingByDuration;
const toMinutes = (time) => time.hour * 60 + time.minutes;
exports.toMinutes = toMinutes;
const sumTimes = (timeA, timeB) => {
    const summedMinutes = timeA.minutes + timeB.minutes;
    return {
        hour: timeA.hour + timeB.hour + Math.floor(summedMinutes / 60),
        minutes: summedMinutes % 60
    };
};
exports.sumTimes = sumTimes;
const displayTime = (time) => `${time.hour}:${(0, exports.displayMinutes)(time.minutes)}`;
exports.displayTime = displayTime;
const displayMinutes = (minutes) => minutes < 10 ? `0${minutes}` : minutes.toString();
exports.displayMinutes = displayMinutes;


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
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared","schedule_shared"], () => (__webpack_exec__("./React/schedule-arranger.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=schedule_arranger.bundle.js.map