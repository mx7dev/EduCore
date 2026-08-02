export interface ResponseDto<T> {
  success: boolean;
  message?: string;
  data?: T;
  code?: string;
  transactionId?: string;
}