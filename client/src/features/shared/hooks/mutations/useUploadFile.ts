import { useMutation } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type { BaseError } from "../../types/baseError.types";
import type { UploadFileResponse } from "../../types/files/uploadFile.types";
import { postFile } from "../../apis/files/uploadFile.api";

export const useUploadFile = () =>
  useMutation<UploadFileResponse, AxiosError<BaseError>, File>({
    mutationFn: postFile,
  });
