import { CheckCircle } from "lucide-react";
import { useForm } from "react-hook-form";
import type { LogInForm } from "../type";
import { zodResolver } from "@hookform/resolvers/zod";
import { logInFormSchema } from "../schema";

const LoginForm = () => {
  const form = useForm<LogInForm>({
    resolver: zodResolver(logInFormSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const handleSubmit = form.handleSubmit((data) => {
    console.log(data);
    // form.setError("root", {
    //   type: "server",
    //   message: "Đăng nhập thất bại. Vui lòng thử lại.",
    // });
  });

  // After sending data to API successfully, render this UI
  if (form.formState.isSubmitSuccessful) {
    return (
      <>
        <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4">
          <div className="bg-white p-8 rounded-xl shadow-lg max-w-md w-full text-center">
            <CheckCircle className="w-16 h-16 text-green-500 mx-auto mb-4" />
            <h2 className="text-2xl font-bold text-gray-900 mb-2">
              Welcome back
            </h2>
            <p className="text-gray-600 mb-4">
              You have been successfully logged in
            </p>
            <div className="animate-spin w-6 h-6 border-2 border-blue-600 border-t-transparent rounded-full mx-auto"></div>
            <p className="text-sm text-gray-500 mt-2">
              Redirecting to your dashboard...
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
            <p className="text-gray-600">Sign in to your Job Portal account</p>
          </div>

          <form onSubmit={handleSubmit} className="space-y-6">
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

            {form.formState.errors.root && (
              <p className="text-sm text-red-600 text-center">
                {String(form.formState.errors.root.message)}
              </p>
            )}

            {/* Submit button */}
            <button
              disabled={form.formState.isSubmitting || !form.formState.isValid}
              type="submit"
              className="w-full bg-gradient-to-r from-blue-600 to-purple-600 text-white py-3 rounded-lg font-semibold hover:from-blue-700 hover:to-purple-700  transition-all duration-300 disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center space-x-2"
            >
              Log in
            </button>
          </form>

          {/* Sign up link */}
          <div className="text-center mt-6">
            <p className="text-gray-600">
              Don't have an account?{" "}
              <a className="text-blue-600 hover:text-blue-700 font-medium">
                Create one here
              </a>
            </p>
          </div>
        </div>
      </div>
    </>
  );
};

export default LoginForm;
