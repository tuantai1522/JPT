import { twMerge } from "tailwind-merge";

type ButtonProps = {
  className?: string;
} & React.ButtonHTMLAttributes<HTMLButtonElement>;

export default function Button({ children, className, ...props }: ButtonProps) {
  const base =
    "bg-gradient-to-r from-blue-600 to-purple-600 text-white px-6 py-2 rounded-lg font-medium transition-all duration-300 shadow-sm hover:from-blue-700 hover:to-purple-700 hover:shadow-md disabled:opacity-60 disabled:cursor-not-allowed shalow-lg hover:shadow-xl flex items-center space-x-2";

  return (
    <button {...props} className={twMerge(base, className)}>
      {children}
    </button>
  );
}
