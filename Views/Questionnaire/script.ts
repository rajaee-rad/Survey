// define(function () {
//     class QuestionnaireForm {
//         constructor(service, templateEl, questionnaireId) {
//             this._service = service;
//             this._templateEl = templateEl;
//             this._containerEl = $(".questionnaire", this._templateEl);
//             this._title = null;
//             this._questionnaireId = questionnaireId;
//             this._currentQuestions = null;
//             this._currentAnswers = {};
//             this._questionValidations = {};
//             this._button = null;
//         }
//
//         load() {
//             this._loadNext();
//         }
//
//         _render() {
//             let self = this;
//             if (self._currentQuestions == null)
//                 throw new Error("The current steps is not loaded yet.");
//
//             self._renderTitle();
//             self._renderNextButton();
//             self._renderQuestions();
//         }
//         _renderTitle() {
//             console.log(this._title);
//             let questionnaireTitleEl = document.querySelector(".questionnaire-title");
//             questionnaireTitleEl.innerText = this._title;
//         }
//         _renderQuestions() {
//             let self = this;
//             let questionCounter = 0;
//
//             if (self._currentQuestions == null) {
//                 $(".no-questions", this._templateEl).css("display", "block");
//                 $(".btn-nextstep", this._templateEl).css("display", "none");
//                 return;
//             }
//             self._currentQuestions.forEach(question => {
//                 questionCounter++;
//                 let questionContainer = document.createElement("div");
//                 questionContainer.className = "question-container";
//                 self._renderQuestion(question, questionContainer, questionCounter);
//                 self._currentAnswers[question.QuestionId] = null;
//
//                 switch (question.QuestionType) {
//                     case 1:
//                         self._renderChoosableAnswer(question, questionContainer);
//                         break;
//                     case 2:
//                         self._renderScoreAnswer(question, questionContainer);
//                         break;
//                     case 3:
//                         self._renderTextAnswer(question, questionContainer);
//                         break;
//                     case 4:
//                         self._renderNumberAnswer(question, questionContainer);
//                         break;
//                     default:
//                         throw new Error(`The question type ${question.QuestionType} is not supported.`);
//                 }
//             });
//             self._ensureValidate();
//         }
//         _renderQuestion(question, questionContainer, questionCounter) {
//
//             let questionEl = document.createElement("p");
//             questionEl.setAttribute("data-id", question.QuestionId);
//             questionEl.appendChild(document.createTextNode(questionCounter +"- " + question.Title + "؟"));
//             if (question.IsRequire) {
//                 let requiredEl = document.createElement("span");
//                 requiredEl.className = "required";
//                 requiredEl.innerText ="*";
//
//                 questionEl.appendChild(requiredEl);
//                 this._questionValidations[question.QuestionId] = {
//                     isValid: false
//                 };
//             }
//             questionContainer.appendChild(questionEl);
//             if (question.Description != null) {
//                 let questionDescriptionEl = document.createElement("p");
//                 questionDescriptionEl.className = "question-description";
//                 questionDescriptionEl.innerText = question.Description;
//                 questionContainer.appendChild(questionDescriptionEl);
//             }
//             this._containerEl.append(questionContainer);
//         }
//         _renderChoosableAnswer(question, questionContainer) {
//             if (question.SingleSelect)
//                 this._renderRadioAnswer(question, questionContainer, question.QuestionDetail.Data.Options);
//             else
//                 this._renderCheckBoxAnswer(question, questionContainer, question.QuestionDetail.Data.Options);
//
//             if (question.NeedDescription) {
//                 let answerDescriptionTitleEl = document.createElement("span");
//                 answerDescriptionTitleEl.className = "answer-description-title";
//                 answerDescriptionTitleEl.innerText = "پاسخ خود را شرح دهید:";
//                 questionContainer.appendChild(answerDescriptionTitleEl);
//                 let instance = bigBangUI.textAreaEditor.create({
//                     renderTo: questionContainer,
//                     direction: "rtl",
//                     maxLength: 500,
//                     onValueChanged: () => {
//                         this._getQuestionAnswer(question, "Text").Data.Description = instance.value;
//                     }
//                 });
//             }
//         }
//         _renderRadioAnswer(question, questionContainer, options) {
//             let counter = 1;
//             // let option = [];
//             let answerItemsContainer = document.createElement("div");
//             answerItemsContainer.className = "answer-items-container bb-scroll-bar";
//             options.forEach(option => {
//                 counter++;
//                 let answerItem = document.createElement("div");
//                 answerItem.className = "answer-item bb-scroll-bar";
//                 let answerRadioEl = document.createElement("input");
//                 let labelRadioEl = document.createElement("label");
//                 answerRadioEl.className = "radio-item"
//                 answerRadioEl.type = "radio";
//                 answerRadioEl.name = question.QuestionId;
//                 answerRadioEl.id = 'qu' + counter;
//                 answerRadioEl.addEventListener("change", () => {
//                     this._updateChoosableAnswer(question, "replace", option);
//                     this._updateChoosableAnswerStyle(question ,answerRadioEl);
//                 });
//
//                 labelRadioEl.htmlFor = 'qu' + counter;
//                 labelRadioEl.appendChild(document.createTextNode(option));
//                 answerItem.append(answerRadioEl);
//                 answerItem.append(labelRadioEl);
//                 answerItemsContainer.append(answerItem);
//             });
//             questionContainer.append(answerItemsContainer);
//         }
//         _updateChoosableAnswerStyle(question, answerEl) {
//             let questionEl = $(`p[data-id='${question.QuestionId}']`);
//             let answerContainer = $(questionEl).nextAll('.answer-items-container');
//             let previusSelectedAnswer = $(answerContainer).find('.selected');
//             console.log(previusSelectedAnswer);
//             if ($(answerEl.checked)) {
//                 if (previusSelectedAnswer.length > 0)
//                     $(answerContainer).find('.selected').removeClass("selected");
//                 answerEl.parentElement.classList.add("selected");
//             }
//         }
//         _renderCheckBoxAnswer(question, questionContainer, options) {
//             let answerItemsContainer = document.createElement("div");
//             answerItemsContainer.className = "answer-items-container bb-scroll-bar";
//             options.forEach(option => {
//                 let answerItem = document.createElement("div");
//                 answerItem.className = "answer-item bb-scroll-bar";
//                 let instance = bigBangUI.checkbox.create({
//                     renderTo: answerItem,
//                     replace: false,
//                     mode: "classic",
//                     scale: 'small',
//                     scapeAction: 'reset',
//                     label: option,
//                     direction: "rtl",
//                     tooltip: "",
//                     onValueChanged: () => {
//                         this._updateChoosableAnswer(question, instance.checked ? "add" : "remove", option);
//                     }
//                 });
//                 answerItemsContainer.append(answerItem);
//             });
//             questionContainer.append(answerItemsContainer);
//         }
//         _updateChoosableAnswer(question, action, option) {
//             let answer = this._getQuestionAnswer(question, "MultiChoice");
//
//             if (option != undefined) {
//                 if (answer.Data.Value == null)
//                     answer.Data.Value = [];
//
//                 switch (action) {
//                     case "add":
//                         answer.Data.Value.push(option);
//                         if (question.IsRequire)
//                             this._invalidateQuestion(question.QuestionId, true);
//                         break;
//                     case "remove":
//                         var index = answer.Data.Value.indexOf(option);
//                         answer.Data.Value.splice(index, 1);
//                         break;
//                     //radio button
//                     case "replace":
//                         if (answer.Data.Value.length == 0) {
//                             answer.Data.Value.push(option);
//                             if (question.IsRequire)
//                                 this._invalidateQuestion(question.QuestionId, true);
//                         }
//                         else {
//                             answer.Data.Value[0] = option;
//                             if (question.IsRequire)
//                                 this._invalidateQuestion(question.QuestionId, true);
//                         }
//                         break;
//                 }
//             }
//         }
//         _renderNumberAnswer(question, questionContainer) {
//             let instance = bigBangUI.numberEditor.create({
//                 renderTo: questionContainer,
//                 direction: "ltr",
//                 replace: false,
//                 onValueChanged: () => {
//                     this._getQuestionAnswer(question, "Number").Data.Value = instance.value == null ? null : instance.value;
//                     if (question.IsRequire)
//                         this._invalidateQuestion(question.QuestionId, instance.value != null);
//                 }
//             });
//         }
//         _renderScoreAnswer(question, questionContainer) {
//             let scoreGroupElement = document.createElement('div');
//             scoreGroupElement.className = 'star-group';
//             let values = [5, 4, 3, 2, 1];
//             values.forEach((value) => {
//                 let starElement = document.createElement('input');
//                 let starLabel = document.createElement('label');
//                 starElement.type = 'radio';
//                 starElement.className = 'star';
//                 starElement.name = question.QuestionId;
//                 starElement.value = value;
//                 starLabel.htmlFor = "sa" + value;
//                 starLabel.id = "sa" + value;
//                 starLabel.className = 'star-label fas fa-star fa-fw';
//                 starLabel.addEventListener("click", () => {
//                     starElement.checked = true;
//                     this._getQuestionAnswer(question, "Score").Data.Value = value;
//                     if (question.IsRequire)
//                         this._invalidateQuestion(question.QuestionId, true);
//                 });
//                 scoreGroupElement.append(starElement);
//                 scoreGroupElement.append(starLabel);
//             });
//             questionContainer.append(scoreGroupElement);
//         }
//         _renderTextAnswer(question, questionContainer) {
//             let instance = bigBangUI.textAreaEditor.create({
//                 renderTo: questionContainer,
//                 direction: "ltr",
//                 maxLength: 500,
//                 onValueChanged: () => {
//                     this._getQuestionAnswer(question, "Text").Data.Value = instance.value == null ? null : instance.value;
//                     if (question.IsRequire)
//                         this._invalidateQuestion(question.QuestionId, instance.value != null);
//                 }
//             });
//         }
//         _renderNextButton() {
//             this._button = bigBangUI.button.create({
//                 renderTo: $(".btn-nextstep", self._templateEl),
//                 replace: false,
//                 scale: 'medium',
//                 text: "گام بعد ",
//                 icon: "",
//                 onClicked: () => {
//                     this._loadNext();
//                 },
//                 tooltip: "گام بعد"
//             });
//         }
//
//         _getQuestionAnswer(question, netType) {
//             let answer = this._currentAnswers[question.QuestionId];
//             if (answer == null) {
//                 answer = {
//                     Data: {
//                         $NetType: netType,
//                         Value: null
//                     },
//                     hasValue: () => {
//                         if (answer.Data == null)
//                             return false;
//
//                         let hasValue = false;
//                         Object.keys(answer.Data).forEach(key => {
//                             if (key == "$NetType")
//                                 return;
//                             let propValue = answer.Data[key];
//                             if (Array.isArray(propValue) && propValue.length > 0) {
//                                 hasValue = true;
//                                 return;
//                             }
//                             if (propValue != null) {
//                                 hasValue = true;
//                                 return;
//                             }
//                         });
//                         return hasValue;
//                     }
//                 };
//                 this._currentAnswers[question.QuestionId] = answer;
//             }
//             return answer;
//         }
//
//         _invalidateQuestion(questionId, isValid) {
//             let el = $(`p[data-id='${questionId}']`, this._templateEl);
//             el.css("color", isValid ? "" : "red");
//             this._questionValidations[questionId].isValid = isValid;
//             this._ensureValidate();
//         }
//         _ensureValidate() {
//             let isValid = true;
//             Object.keys(this._questionValidations).forEach(questionId => {
//                 if (!this._questionValidations[questionId].isValid) {
//                     isValid = false;
//                     return;
//                 }
//             });
//             this._button.enabled = isValid;
//         }
//
//         _reset() {
//             this._currentAnswers = {};
//             this._questionValidations = {};
//             this._currentQuestions = null;
//
//             $(".questionnaire", this._templateEl).empty();
//         }
//
//         _loadNext() {
//             let self = this;
//             let answers = null;
//             if (Object.keys(self._currentAnswers).length != 0) {
//                 answers = [];
//                 Object.keys(self._currentAnswers).forEach(key => {
//                     if (self._currentAnswers[key] != null && self._currentAnswers[key].hasValue())
//                         answers.push({
//                             QuestionId: key,
//                             Answer: self._currentAnswers[key]
//                         });
//                 });
//             }
//
//             self._service.call("GetNext", {
//                 questionnaireId: self._questionnaireId,
//                 currentStepId: self._currentQuestions == null ? null : self._currentQuestions[self._currentQuestions.length - 1].CurrentStepId,
//                 answers: answers
//             }).then(response => {
//                 let firstStep = self._currentQuestions == null;
//                 self._reset();
//                 self._currentQuestions = response;
//                 if (firstStep) {
//                     self._service.call("GetTitle", {
//                         questionnaireId: self._questionnaireId
//                     }).then(title => {
//                         self._title = title;
//                         self._render();
//                     });
//                 }
//                 else
//                     self._renderQuestions();
//             });
//         }
//     }
//
//     return {
//         /**
//          * The editor service for communication with server
//          * @typedef {Object} service
//          * @property {function(string): Promise} call
//          */
//
//         /**
//          * Custom editor configuration object
//          * @typedef {Object} config
//          * @property {function(string)} resolveService Finding a runner service with service name
//          * @property {string} templateEl The main HTML template
//          * @property {Object<string, string>} relativeTemplates The relative HTML templates
//          * @property {Object<string, Object>} relativeModules The relative modules
//          * @property {Object} service {@link service} Custom service instance for communicate with server
//          */
//
//         /**
//          * Layout factory method
//          * @param {Object} config {@link config} The editor configuration
//          * @return {Object} editor instance
//          */
//         create: function (config) {
//             // console.log("config: " + config.);
//             let template = $(config.templateEl);
//             let questionnaireId = config.storeRelation.getBusinessContext().QuestionnaireId;
//             let form = new QuestionnaireForm(config.service, template, questionnaireId);
//             form.load();
//             config.containerEl.append(template);
//
//             return {
//                 activationStatusChanged: function (isActive) {
//
//                 }
//             }
//         }
//     };
// });
//
//
