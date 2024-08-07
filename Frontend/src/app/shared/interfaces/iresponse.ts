export interface IResponse<T>{
  statusCode: number;
  message: string;
  payload: T;
  isSuccess: boolean;
}
