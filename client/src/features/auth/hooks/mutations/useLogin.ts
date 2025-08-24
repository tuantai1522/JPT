import { useMutation } from "@tanstack/react-query";
import { login } from "../../apis/api.login";
import type { LoginRequest, LoginResponse } from "../../types/login";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError";

export const useLogin = () =>
  useMutation<LoginResponse, AxiosError<BaseError>, LoginRequest>({
    mutationFn: login,
  });
