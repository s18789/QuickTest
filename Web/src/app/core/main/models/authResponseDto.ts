export interface AuthResponseDto {
  isAuthSuccessful: boolean,
  errorMessage: string,
  token: string,
  userId: string,
  schoolId: number
}

export interface UserInfoResponse {
  userId: string,
  firstName: string,
  lastName: string,
  email: string,
  userName: string
}