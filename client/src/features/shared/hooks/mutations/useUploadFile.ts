import { useMutation } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError";
import type { UploadFileResponse } from "../../types/files/uploadFile";
import { postFile } from "../../apis/api.uploadFile";

export const useUploadFile = () =>
  useMutation<UploadFileResponse, AxiosError<BaseError>, File>({
    mutationFn: postFile,
  });
