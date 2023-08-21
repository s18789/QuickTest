import { FormControl, FormGroup } from "@angular/forms";

export interface StudentFormGroup {
  firstName: FormControl,
  lastName: FormControl,
  email: FormControl,
  group: FormGroup<GroupFormGroup>
}

export interface GroupFormGroup {
  id: FormControl
}
