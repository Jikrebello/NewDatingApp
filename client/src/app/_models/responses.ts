export interface BaseResponse {
  success: boolean;
  message: string;
}

export interface ResultResponse<T> extends BaseResponse {
  result: T;
}
