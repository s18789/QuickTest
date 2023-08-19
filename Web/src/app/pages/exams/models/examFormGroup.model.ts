import { FormControl, FormArray, FormGroup } from "@angular/forms";

export interface ExamFormGroup {
  title: FormControl,
  time: FormControl,
  availableFrom: FormControl,
  availableTo: FormControl,
  questions: FormArray<FormGroup<QuestionFormGroup>>,
  students: FormArray<FormGroup<StudentFormGroup>>,
}

export interface QuestionFormGroup {
  points: FormControl,
  type: FormControl,
  questionContent: FormControl,
  answers: FormArray<FormGroup<AnswerFormGroup>>,
}

export interface AnswerFormGroup {
  answerContent: FormControl,
  correct: FormControl;
}

export interface StudentFormGroup {
  id: FormControl,
  // firstName?: FormControl,
  // lastName?: FormControl,
  // isSelected?: FormControl,
}
