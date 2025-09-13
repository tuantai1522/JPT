interface StatSectionProps {
  children: React.ReactNode;
  headerSection: React.ReactNode;
  title: string;
  subTitle: string;
}
export default function StatSection({
  children,
  headerSection,
  title,
  subTitle,
}: StatSectionProps) {
  return (
    <>
      <div
        className={`bg-white rounded-xl border border-gray-300 shadow-sm hover:shadow-md transition-shadow duration-200`}
      >
        <div className="flex items-center justify-between p-6 pb-4">
          <div>
            <h3 className="text-lg font-semibold text-gray-900">{title}</h3>
            <p className="text-sm text-gray-500 mt-1">{subTitle}</p>
          </div>
          {headerSection}
        </div>
        <div className="px-6 pb-6">{children}</div>
      </div>
    </>
  );
}
