import { FormArray, FormControl, FormGroup } from "@angular/forms";

export interface ExamResultFormGroup {
  examId: FormControl<number>,
  examResultId: FormControl<number>,
  questions: FormArray<FormGroup<QuestionFormGroup>>
}

export interface QuestionFormGroup {
  questionId: FormControl<number>
  content: FormControl<string>
  answers: FormArray<FormGroup<AnswerFormGroup>>
}

export interface AnswerFormGroup {
  answerId: FormControl<number>
  content: FormControl<string>
  isSelected: FormControl<boolean>
}
