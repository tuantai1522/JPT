import { useMutation } from "@tanstack/react-query";
import { postLogin } from "../../apis/api.auth";
import type { LoginResponse, LoginRequest } from "../../type";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError";

export const useLoginMutation = () =>
  useMutation<LoginResponse, AxiosError<BaseError>, LoginRequest>({
    mutationFn: postLogin,
  });
