import RecentJobApplications from "./RecentJobApplications";
import RecentActivity from "./RecentJobPosts";
import StatDashboard from "./StatDashboard";

const EmployerDashboard = () => {
  return (
    <>
      <div className="max-w-7xl mx-auto space-y-8">
        <StatDashboard />

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <RecentActivity />
          <RecentJobApplications />
        </div>
      </div>
    </>
  );
};

export default EmployerDashboard;
