export interface AuthResponseDto {
  isAuthSuccessful: boolean,
  errorMessage: string,
  token: string,
  userId: string,
  schoolId: number
}
