import { Observable } from "rxjs";
import { AuthDataModel } from "./authDataModel";
import { AuthResponseDto } from "./authResponseDto";

export interface AuthServiceInterface {
  //logged(): void;
  //logIn(value: AuthDataModel): Observable<AuthResponseDto>;
  logOut(): void;
}
