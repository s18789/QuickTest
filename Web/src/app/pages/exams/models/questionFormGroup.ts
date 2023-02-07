import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { AnswerFormGroup } from "./answerFormGroup";

export interface QuestionFormGroup {
  points: FormControl,
  questionContent: FormControl,
  answers: FormArray<FormGroup<AnswerFormGroup>>,
}
