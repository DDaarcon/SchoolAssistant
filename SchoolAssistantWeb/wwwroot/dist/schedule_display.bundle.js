"use strict";
(this["webpackChunkschoolassistant"] = this["webpackChunkschoolassistant"] || []).push([["schedule_display"],{

/***/ "./React/schedule-display/components/lesson-tiles/lesson-tile.css":
/*!************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/lesson-tile.css ***!
  \************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-display/components/lesson-tiles/student-lesson-tile.css":
/*!********************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/student-lesson-tile.css ***!
  \********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.css":
/*!********************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.css ***!
  \********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-display/components/schedule-display-timeline.css":
/*!*************************************************************************!*\
  !*** ./React/schedule-display/components/schedule-display-timeline.css ***!
  \*************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./React/schedule-display/schedule.css":
/*!*********************************************!*\
  !*** ./React/schedule-display/schedule.css ***!
  \*********************************************/
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

/***/ "./React/schedule-display.ts":
/*!***********************************!*\
  !*** ./React/schedule-display.ts ***!
  \***********************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const schedule_1 = __importDefault(__webpack_require__(/*! ./schedule-display/schedule */ "./React/schedule-display/schedule.tsx"));
globalThis.Components.Schedule = schedule_1.default;


/***/ }),

/***/ "./React/schedule-display/components/day-column.tsx":
/*!**********************************************************!*\
  !*** ./React/schedule-display/components/day-column.tsx ***!
  \**********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const day_column_base_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/components/day-column-base */ "./React/schedule-shared/components/day-column-base.tsx"));
const settings_1 = __importDefault(__webpack_require__(/*! ../settings */ "./React/schedule-display/settings.ts"));
const lessons_by_day_1 = __importDefault(__webpack_require__(/*! ./lessons-by-day */ "./React/schedule-display/components/lessons-by-day.tsx"));
const timeline_cell_1 = __importDefault(__webpack_require__(/*! ./timeline-cell */ "./React/schedule-display/components/timeline-cell.tsx"));
class DayColumn extends day_column_base_1.default {
    constructor(props) {
        super(props);
        this.instantiateCells();
    }
    get _cellDuration() { return 60 / settings_1.default.CellsPerHour; }
    instantiateCells() {
        if (!this.getTimelineCellComponent)
            throw new Error("Overriding method `getTimelineCellComponent` is required for calling `instantiateCells`");
        const count = (this.props.config.endHour - this.props.config.startHour) * settings_1.default.CellsPerHour;
        this._cellHeight = this.props.scheduleHeight / count;
        const cellTimes = Array.from({ length: count }, (_, i) => {
            const minutesFromMidnight = (this.props.config.startHour * 60) + this._cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        });
        this._cells = cellTimes.map((cellTime, i) => this.getTimelineCellComponent(cellTime, i));
    }
    getLessonsDisplayComponent() {
        return (react_1.default.createElement(lessons_by_day_1.default, { lessons: this.props.lessons, day: this.props.dayIndicator, config: this.props.config, cellHeight: this._cellHeight }));
    }
    getTimelineCellComponent(time, index) {
        return (react_1.default.createElement(timeline_cell_1.default, { key: index, height: this._cellHeight, dayIndicator: this.props.dayIndicator, cellIndex: index, time: time }));
    }
    render() {
        this.instantiateCells();
        return super.render();
    }
}
exports["default"] = DayColumn;


/***/ }),

/***/ "./React/schedule-display/components/lesson-tiles/lesson-tile.tsx":
/*!************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/lesson-tile.tsx ***!
  \************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const settings_1 = __importDefault(__webpack_require__(/*! ../../settings */ "./React/schedule-display/settings.ts"));
__webpack_require__(/*! ./lesson-tile.css */ "./React/schedule-display/components/lesson-tiles/lesson-tile.css");
// TODO: room name doesn't have numer
// TODO: details panel is hidden behind lesson tile
class LessonTile extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.mouseEntered = () => {
            this.setState({ hover: true });
        };
        this.mouseLeft = () => {
            this.setState({ hover: false });
        };
        this.state = {
            hover: false
        };
    }
    get _cellDuration() { return 60 / settings_1.default.CellsPerHour; }
    calcTopOffset() {
        const minutes = (this.props.lesson.time.hour - this.props.config.startHour) * 60 + this.props.lesson.time.minutes;
        const cells = minutes / this._cellDuration;
        return cells * this.props.cellHeight;
    }
    calcHeight() {
        var _a;
        const duration = (_a = this.props.lesson.customDuration) !== null && _a !== void 0 ? _a : this.props.config.defaultLessonDuration;
        const cells = duration / this._cellDuration;
        return cells * this.props.cellHeight;
    }
    render() {
        let style = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        };
        return (react_1.default.createElement("div", { className: "sched-disp-lesson-tile my-raised-bar", style: style, onMouseEnter: this.mouseEntered, onMouseLeave: this.mouseLeft }, this.getInnerComponents()));
    }
}
exports["default"] = LessonTile;


/***/ }),

/***/ "./React/schedule-display/components/lesson-tiles/student-lesson-tile.tsx":
/*!********************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/student-lesson-tile.tsx ***!
  \********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_functions_1 = __webpack_require__(/*! ../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const form_controls_1 = __webpack_require__(/*! ../../../shared/form-controls */ "./React/shared/form-controls.ts");
const lesson_tile_1 = __importDefault(__webpack_require__(/*! ./lesson-tile */ "./React/schedule-display/components/lesson-tiles/lesson-tile.tsx"));
__webpack_require__(/*! ./student-lesson-tile.css */ "./React/schedule-display/components/lesson-tiles/student-lesson-tile.css");
class StudentLessonTile extends lesson_tile_1.default {
    getInnerComponents() {
        return (react_1.default.createElement("div", { className: "sched-stud-lesson-inner-container" },
            react_1.default.createElement("div", { className: "sched-stud-lesson-subject" }, this.props.lesson.subject.name),
            react_1.default.createElement("div", { className: "sched-stud-lesson-expandable " + (this.state.hover ? "expanded" : "") },
                react_1.default.createElement(form_controls_1.LabelValue, { label: "czas", value: (0, time_functions_1.displayTime)(this.props.lesson.time), containerClassName: "label-value-lesson-details-stud", labelContainerClassName: "lab-val-lab-lesson-details-stud" }),
                react_1.default.createElement(form_controls_1.LabelValue, { label: "miejsce", value: this.props.lesson.room.name, containerClassName: "label-value-lesson-details-stud", labelContainerClassName: "lab-val-lab-lesson-details-stud" }),
                react_1.default.createElement(form_controls_1.LabelValue, { label: "wyk\u0142.", value: this.props.lesson.lecturer.name, containerClassName: "label-value-lesson-details-stud", labelContainerClassName: "lab-val-lab-lesson-details-stud" }))));
    }
}
exports["default"] = StudentLessonTile;


/***/ }),

/***/ "./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.tsx":
/*!********************************************************************************!*\
  !*** ./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.tsx ***!
  \********************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_functions_1 = __webpack_require__(/*! ../../../schedule-shared/help/time-functions */ "./React/schedule-shared/help/time-functions.ts");
const form_controls_1 = __webpack_require__(/*! ../../../shared/form-controls */ "./React/shared/form-controls.ts");
const lesson_tile_1 = __importDefault(__webpack_require__(/*! ./lesson-tile */ "./React/schedule-display/components/lesson-tiles/lesson-tile.tsx"));
__webpack_require__(/*! ./teacher-lesson-tile.css */ "./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.css");
class TeacherLessonTile extends lesson_tile_1.default {
    getInnerComponents() {
        var _a, _b, _c;
        const studentClassName = (_b = (_a = this.props.lesson.orgClass) === null || _a === void 0 ? void 0 : _a.name) !== null && _b !== void 0 ? _b : (_c = this.props.lesson.subjClass) === null || _c === void 0 ? void 0 : _c.name;
        return (react_1.default.createElement("div", { className: "sched-teac-lesson-inner-container" },
            react_1.default.createElement("div", { className: "sched-teac-lesson-main-cnt" }, `${studentClassName} ${this.props.lesson.subject.name}`),
            react_1.default.createElement("div", { className: "sched-teac-lesson-expandable " + (this.state.hover ? "expanded" : "") },
                react_1.default.createElement(form_controls_1.LabelValue, { label: "czas", value: (0, time_functions_1.displayTime)(this.props.lesson.time), containerClassName: "label-value-lessson-details-teac", labelContainerClassName: "lab-val-lab-lesson-details-teac" }),
                react_1.default.createElement(form_controls_1.LabelValue, { label: "miejsce", value: this.props.lesson.room.name, containerClassName: "label-value-lessson-details-teac", labelContainerClassName: "lab-val-lab-lesson-details-teac" }))));
    }
}
exports["default"] = TeacherLessonTile;


/***/ }),

/***/ "./React/schedule-display/components/lessons-by-day.tsx":
/*!**************************************************************!*\
  !*** ./React/schedule-display/components/lessons-by-day.tsx ***!
  \**************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const schedule_viewer_type_1 = __importDefault(__webpack_require__(/*! ../enums/schedule-viewer-type */ "./React/schedule-display/enums/schedule-viewer-type.ts"));
const student_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./lesson-tiles/student-lesson-tile */ "./React/schedule-display/components/lesson-tiles/student-lesson-tile.tsx"));
const teacher_lesson_tile_1 = __importDefault(__webpack_require__(/*! ./lesson-tiles/teacher-lesson-tile */ "./React/schedule-display/components/lesson-tiles/teacher-lesson-tile.tsx"));
class LessonsByDay extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.setProperTileComponent();
    }
    render() {
        return (react_1.default.createElement(react_1.default.Fragment, null, this.props.lessons.map(lesson => react_1.default.createElement(this._tileComponent, { key: `${lesson.time.hour}${lesson.time.minutes}`, config: this.props.config, lesson: lesson, cellHeight: this.props.cellHeight }))));
    }
    setProperTileComponent() {
        switch (this.props.config.for) {
            case schedule_viewer_type_1.default.Student:
                this._tileComponent = student_lesson_tile_1.default;
                break;
            case schedule_viewer_type_1.default.Teacher:
                this._tileComponent = teacher_lesson_tile_1.default;
                break;
        }
    }
}
exports["default"] = LessonsByDay;


/***/ }),

/***/ "./React/schedule-display/components/schedule-display-timeline.tsx":
/*!*************************************************************************!*\
  !*** ./React/schedule-display/components/schedule-display-timeline.tsx ***!
  \*************************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const time_column_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/components/time-column */ "./React/schedule-shared/components/time-column.tsx"));
const time_column_variant_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/enums/time-column-variant */ "./React/schedule-shared/enums/time-column-variant.ts"));
const schedule_timeline_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/schedule-timeline */ "./React/schedule-shared/schedule-timeline.tsx"));
const day_column_1 = __importDefault(__webpack_require__(/*! ./day-column */ "./React/schedule-display/components/day-column.tsx"));
__webpack_require__(/*! ./schedule-display-timeline.css */ "./React/schedule-display/components/schedule-display-timeline.css");
class ScheduleDisplayTimeline extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.dayColumnFactory = (day) => {
            var _a, _b;
            const lessons = (_b = (_a = this.props.data.find(x => x.dayIndicator == day)) === null || _a === void 0 ? void 0 : _a.lessons) !== null && _b !== void 0 ? _b : [];
            return (react_1.default.createElement(day_column_1.default, { key: day, scheduleHeight: this.state.scheduleHeight, config: this.props.config, dayIndicator: day, lessons: lessons }));
        };
        this.HIDDEN_CLASS_NAME = "schedule-hidden";
        this.state = {
            scheduleHeight: 200
        };
    }
    render() {
        return (react_1.default.createElement("div", { className: this._fullClassName, ref: ref => this._containerRef = ref },
            react_1.default.createElement(schedule_timeline_1.default, { config: this.props.config, dayColumnFactory: this.dayColumnFactory, timeColumn: react_1.default.createElement(time_column_1.default, Object.assign({}, this.props.config, { scheduleHeight: this.state.scheduleHeight, variant: time_column_variant_1.default.WholeHoursByHeight })), getReferenceOnMount: ref => this._scheduleRef = ref })));
    }
    get _fullClassName() {
        let className = `schedule-display-timeline-container ${this.HIDDEN_CLASS_NAME}`;
        return className;
    }
    componentDidMount() {
        this._containerRef.classList.remove(this.HIDDEN_CLASS_NAME);
        this.setState({ scheduleHeight: this._scheduleRef.clientHeight });
    }
}
exports["default"] = ScheduleDisplayTimeline;


/***/ }),

/***/ "./React/schedule-display/components/timeline-cell.tsx":
/*!*************************************************************!*\
  !*** ./React/schedule-display/components/timeline-cell.tsx ***!
  \*************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
const timeline_cell_base_1 = __importDefault(__webpack_require__(/*! ../../schedule-shared/components/day-column/timeline-cell-base */ "./React/schedule-shared/components/day-column/timeline-cell-base.tsx"));
class TimelineCell extends timeline_cell_base_1.default {
}
exports["default"] = TimelineCell;


/***/ }),

/***/ "./React/schedule-display/enums/schedule-viewer-type.ts":
/*!**************************************************************!*\
  !*** ./React/schedule-display/enums/schedule-viewer-type.ts ***!
  \**************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
var ScheduleViewerType;
(function (ScheduleViewerType) {
    ScheduleViewerType[ScheduleViewerType["Student"] = 0] = "Student";
    ScheduleViewerType[ScheduleViewerType["Teacher"] = 1] = "Teacher";
})(ScheduleViewerType || (ScheduleViewerType = {}));
exports["default"] = ScheduleViewerType;


/***/ }),

/***/ "./React/schedule-display/schedule.tsx":
/*!*********************************************!*\
  !*** ./React/schedule-display/schedule.tsx ***!
  \*********************************************/
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
const react_1 = __importDefault(__webpack_require__(/*! react */ "./node_modules/react/index.js"));
const loader_1 = __importStar(__webpack_require__(/*! ../shared/loader */ "./React/shared/loader.tsx"));
const schedule_display_timeline_1 = __importDefault(__webpack_require__(/*! ./components/schedule-display-timeline */ "./React/schedule-display/components/schedule-display-timeline.tsx"));
__webpack_require__(/*! ./schedule.css */ "./React/schedule-display/schedule.css");
class Schedule extends react_1.default.Component {
    constructor(props) {
        super(props);
        this.state = {
            showLoader: true
        };
    }
    render() {
        return (react_1.default.createElement("div", { className: "schedule-display-conainer" },
            react_1.default.createElement(schedule_display_timeline_1.default, { config: this.props.config, data: this.props.lessons }),
            this._loader));
    }
    get _loader() {
        return this.state.showLoader ?
            react_1.default.createElement(loader_1.default, { enable: true, type: loader_1.LoaderType.Absolute, size: loader_1.LoaderSize.Medium, className: "schedule-display-loader", ref: ref => this._loaderRef = ref })
            : react_1.default.createElement(react_1.default.Fragment, null);
    }
    componentDidMount() {
        this._loaderRef.classList.add('schedule-display-loader-hide');
        setTimeout(() => {
            this.setState({ showLoader: false });
        }, 1500);
    }
}
exports["default"] = Schedule;


/***/ }),

/***/ "./React/schedule-display/settings.ts":
/*!********************************************!*\
  !*** ./React/schedule-display/settings.ts ***!
  \********************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
const SETTINGS = {
    CellsPerHour: 1
};
exports["default"] = SETTINGS;


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


/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor","react_lib","shared","schedule_shared"], () => (__webpack_exec__("./React/schedule-display.ts")));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=schedule_display.bundle.js.map