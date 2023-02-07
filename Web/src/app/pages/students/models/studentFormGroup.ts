import { FormControl } from "@angular/forms";

export interface StudentFormGroup {
  firstName: FormControl,
  lastName: FormControl,
  email: FormControl,
  groupId: FormControl
}
