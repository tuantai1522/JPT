import { Building2, CheckCircle, Upload, User, UserCheck } from "lucide-react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useUploadFile } from "../../shared/hooks/mutations/useUploadFile";
import { useRegister } from "../hooks/mutations/useRegister";
import { useNavigate } from "@tanstack/react-router";
import {
  registerFormSchema,
  type RegisterFormSchema,
} from "../schemas/register.schema";
import { UserRole } from "../../shared/constants/userRole";

const RegisterForm = () => {
  const form = useForm<RegisterFormSchema>({
    resolver: zodResolver(registerFormSchema),
    defaultValues: {
      firstName: "",
      email: "",
      password: "",
      confirmPassword: "",
      companyName: "",
      role: UserRole.JobSeeker,
    },
  });

  const uploadFileMutation = useUploadFile();
  const registerMutation = useRegister();
  const navigate = useNavigate();

  const handleSubmit = form.handleSubmit((data) => {
    registerMutation.mutateAsync(data, {
      onSuccess: (data) => {
        console.log("Đăng ký thành công", data);
      },
      onError: (err) => {
        const data = err.response?.data;
        form.setError("root", {
          type: "server",
          message:
            data?.errors && data?.errors.length > 0
              ? data?.errors.map((e) => e.description).join("\n")
              : data?.detail,
        });
      },
    });
  });

  const handleUploadFile = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const data = e.target.files?.[0];
    if (!data) {
      return;
    }

    uploadFileMutation.mutateAsync(data, {
      onSuccess: (data) => {
        console.log("Tải fiile thành công", data);

        // Set AvatarId
        form.setValue("avatarId", data.fileId, {
          shouldValidate: true,
          shouldDirty: true,
        });

        // Set AvatarPath
        form.setValue("avatarPath", data.path);
      },
      onError: (err) => {
        const data = err.response?.data;
        form.setError("root", {
          type: "server",
          message:
            data?.errors && data?.errors.length > 0
              ? data?.errors.map((e) => e.description).join("\n")
              : data?.detail,
        });
      },
    });
  };

  if (registerMutation.isSuccess) {
    return (
      <>
        <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4">
          <div className="bg-white p-8 rounded-xl shadow-lg max-w-md w-full text-center">
            <CheckCircle className="w-16 h-16 text-green-500 mx-auto mb-4" />
            <h2 className="text-2xl font-bold text-gray-900 mb-2">
              Account created!
            </h2>
            <p className="text-gray-600 mb-4">
              Your account has been registered successfully
            </p>
            <div className="animate-spin w-6 h-6 border-2 border-blue-600 border-t-transparent rounded-full mx-auto"></div>
            <p className="text-sm text-gray-500 mt-2">
              Please provide me your new email and password to log in...
            </p>
          </div>
        </div>
      </>
    );
  }

  return (
    <>
      <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4">
        <div className="bg-white p-8 rounded-xl shadow-lg max-w-md w-full">
          <div className="text-center mb-8">
            <h2 className="text-2xl font-bold text-gray-900 mb-2">
              Welcome back
            </h2>
            <p className="text-gray-600">Sign up your new Job Portal account</p>
          </div>

          <form onSubmit={handleSubmit} className="">
            <div className="flex gap-6 flex-col">
              {/* User name */}
              <div className="flex gap-4">
                <div className="relative w-full">
                  <label
                    htmlFor="firstName"
                    className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                  >
                    First name
                    <span className="text-red-500">*</span>
                  </label>
                  <input
                    id="firstName"
                    {...form.register("firstName")}
                    className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                  />

                  {form.formState.errors.firstName && (
                    <p id="email" className="mt-1 text-sm text-red-500">
                      {form.formState.errors.firstName.message}
                    </p>
                  )}
                </div>

                <div className="relative w-full">
                  <label
                    htmlFor="middleName"
                    className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                  >
                    Middle name
                  </label>
                  <input
                    id="middleName"
                    {...form.register("middleName")}
                    className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                  />

                  {form.formState.errors.middleName && (
                    <p id="email" className="mt-1 text-sm text-red-500">
                      {form.formState.errors.middleName.message}
                    </p>
                  )}
                </div>

                <div className="relative w-full">
                  <label
                    htmlFor="lastName"
                    className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                  >
                    Last name
                  </label>
                  <input
                    id="lastName"
                    {...form.register("lastName")}
                    className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                  />

                  {form.formState.errors.lastName && (
                    <p id="email" className="mt-1 text-sm text-red-500">
                      {form.formState.errors.lastName.message}
                    </p>
                  )}
                </div>
              </div>

              {/* Email */}
              <div className="relative w-full">
                <label
                  htmlFor="email"
                  className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                >
                  Email
                  <span className="text-red-500">*</span>
                </label>
                <input
                  id="email"
                  {...form.register("email")}
                  className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                />

                {form.formState.errors.email && (
                  <p id="email" className="mt-1 text-sm text-red-500">
                    {form.formState.errors.email.message}
                  </p>
                )}
              </div>

              {/* Password */}
              <div className="relative w-full">
                <label
                  htmlFor="password"
                  className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                >
                  Password
                  <span className="text-red-500">*</span>
                </label>
                <input
                  id="password"
                  {...form.register("password")}
                  className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                />

                {form.formState.errors.password && (
                  <p id="password" className="mt-1 text-sm text-red-500">
                    {form.formState.errors.password.message}
                  </p>
                )}
              </div>

              {/* Confirm Password */}
              <div className="relative w-full">
                <label
                  htmlFor="confirmPassword"
                  className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
                >
                  Confirm password
                  <span className="text-red-500">*</span>
                </label>
                <input
                  id="confirmPassword"
                  {...form.register("confirmPassword")}
                  className="w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white"
                />

                {form.formState.errors.confirmPassword && (
                  <p id="confirmPassword" className="mt-1 text-sm text-red-500">
                    {form.formState.errors.confirmPassword.message}
                  </p>
                )}
              </div>

              {/* Upload file */}
              <div className="relative w-full">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Upload avatar
                </label>

                <div className="flex items-center space-x-4">
                  <div className="w-16 h-16 rounded-full bg-gray-100 flex items-center justify-center overflow-hidden">
                    {form.watch("avatarPath") ? (
                      <img
                        src={form.watch("avatarPath")}
                        alt="avatar"
                        className="w-full h-full object-cover"
                      />
                    ) : (
                      <User className="w-8 h-8 text-gray-400" />
                    )}
                  </div>
                  <div className="flex-1">
                    <input
                      onChange={handleUploadFile}
                      id="avatarId"
                      type="file"
                      className="hidden"
                    />

                    <label
                      htmlFor="avatarId"
                      className="cursor-pointer bg-gray-50 border border-gray-300 rounde-lg px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-100 transition-colors flex items-center space-x-2"
                    >
                      <Upload className="w-4 h-4" />
                      <span>Choose a file</span>
                    </label>
                    <p className="text-xs text-gray-500 mt-1">
                      JPG, PNG upt to 5MB
                    </p>
                  </div>
                </div>

                {form.formState.errors.avatarId && (
                  <p
                    id="avatarId"
                    className="mt-1 text-sm text-red-500 whitespace-pre-line"
                  >
                    {form.formState.errors.avatarId.message}
                  </p>
                )}
              </div>

              <div className="relative w-full">
                <label
                  htmlFor="role"
                  className="block text-sm font-medium text-gray-700 mb-2"
                >
                  Choose a role
                </label>

                <div className="grid grid-cols-2 gap-4" role="radiogroup">
                  {/* Employer */}
                  <div className="relative">
                    <input
                      id="role-employer"
                      type="radio"
                      value="Employer"
                      {...form.register("role")}
                      className="sr-only peer"
                    />
                    <label
                      htmlFor="role-employer"
                      className="
                      cursor-pointer p-4 rounded-lg border-2 transition-all 
                      flex flex-col items-center text-center
                      border-gray-200 hover:border-gray-300
                      peer-checked:border-blue-600 peer-checked:bg-blue-50 peer-checked:shadow-sm
                      peer-checked:font-semibold
                      peer-checked:scale-105"
                    >
                      <Building2 className="w-8 h-8 mx-auto mb-2" />
                      <div className="font-medium">Employer</div>
                      <div className="text-xs text-gray-500">Hiring talent</div>
                    </label>
                  </div>

                  {/* Job Seeker */}
                  <div className="relative">
                    <input
                      id="role-jobseeker"
                      type="radio"
                      value="JobSeeker"
                      {...form.register("role")}
                      className="sr-only peer"
                    />
                    <label
                      htmlFor="role-jobseeker"
                      className="
                        cursor-pointer p-4 rounded-lg border-2 transition-all 
                        flex flex-col items-center text-center
                        border-gray-200 hover:border-gray-300
                        peer-checked:border-blue-600 peer-checked:bg-blue-50 peer-checked:shadow-sm
                        peer-checked:font-semibold
                        peer-checked:scale-105"
                    >
                      <UserCheck className="w-8 h-8 mx-auto mb-2" />
                      <div className="font-medium">Job seeker</div>
                      <div className="text-xs text-gray-500">
                        Looking for opportunities
                      </div>
                    </label>
                  </div>
                </div>
              </div>

              {form.formState.errors.root?.message && (
                <div className="text-red-600 text-sm text-center whitespace-pre-line">
                  {form.formState.errors.root.message}
                </div>
              )}

              {/* Submit button */}
              <button
                disabled={
                  form.formState.isSubmitting || !form.formState.isValid
                }
                type="submit"
                className="w-full bg-gradient-to-r from-blue-600 to-purple-600 text-white py-3 rounded-lg font-semibold hover:cursor-pointer hover:from-blue-700 hover:to-purple-700 transition-all duration-300 disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center space-x-2"
              >
                Create account
              </button>

              {/* Sign in link */}
              <div className="text-center">
                <div className="text-gray-600 flex flex-row items-center justify-center gap-1">
                  <p>Already have an account?</p>
                  <a
                    onClick={() => navigate({ to: "/login" })}
                    className="text-blue-600 hover:text-blue-700 font-medium"
                  >
                    Sign in here
                  </a>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default RegisterForm;
