export interface AuthDataModel {
  username: string;
  password: string;
}

export interface AuthServiceInterface {
  logged(): void;
  logIn(value: AuthDataModel): void;
  logOut(): void;
}
