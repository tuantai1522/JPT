import { useMutation } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError.types";
import type { RegisterRequest } from "../../types/register.types";
import { register } from "../../apis/register.api";

export const useRegister = () =>
  useMutation<string, AxiosError<BaseError>, RegisterRequest>({
    mutationFn: register,
  });
