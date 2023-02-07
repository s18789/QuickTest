import { FormControl, FormArray, FormGroup } from "@angular/forms";
import { QuestionFormGroup } from "./questionFormGroup";
import { StudentFormGroup } from "./studentFormGroup";

export interface ExamFormGroup {
  title: FormControl,
  time: FormControl,
  availableFrom: FormControl,
  availableTo: FormControl,
  questions: FormArray<FormGroup<QuestionFormGroup>>,
  students: FormArray<FormGroup<StudentFormGroup>>,
}
