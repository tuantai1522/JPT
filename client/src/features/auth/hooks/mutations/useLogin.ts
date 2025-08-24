import { useMutation } from "@tanstack/react-query";
import { login } from "../../apis/api.Login";
import type { LoginResponse, LoginRequest } from "../../type";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError";

export const useLogin = () =>
  useMutation<LoginResponse, AxiosError<BaseError>, LoginRequest>({
    mutationFn: login,
  });
