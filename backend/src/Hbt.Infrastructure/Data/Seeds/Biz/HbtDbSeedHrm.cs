//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrm.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM数据初始化主类
//===================================================================

namespace Hbt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// HRM数据初始化主类
/// </summary>
public class HbtDbSeedHrm
{

    private readonly HbtDbSeedHrmDepartment _departmentSeeder;
    private readonly HbtDbSeedHrmPosition _positionSeeder;
    private readonly HbtDbSeedHrmEmployee _employeeSeeder;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedHrm(

        HbtDbSeedHrmDepartment departmentSeeder,
        HbtDbSeedHrmPosition positionSeeder,

        HbtDbSeedHrmEmployee employeeSeeder,
        IHbtLogger logger)
    {

        _departmentSeeder = departmentSeeder;
        _positionSeeder = positionSeeder;

        _employeeSeeder = employeeSeeder;
        _logger = logger;
    }

    /// <summary>
    /// 初始化所有HRM数据
    /// </summary>
    public async Task InitializeHrmDataAsync()
    {
        _logger.Info("开始初始化HRM数据...");

        try
        {


            // 1. 初始化部门
            _logger.Info("初始化部门数据...");
            var (deptInsert, deptUpdate) = await _departmentSeeder.InitializeDepartmentAsync();
            _logger.Info($"部门初始化完成 - 插入: {deptInsert}, 更新: {deptUpdate}");

            // 2. 初始化职位
            _logger.Info("初始化职位数据...");
            var (posInsert, posUpdate) = await _positionSeeder.InitializePositionAsync();
            _logger.Info($"职位初始化完成 - 插入: {posInsert}, 更新: {posUpdate}");





            // 3. 初始化员工数据
            _logger.Info("初始化员工数据...");
            var (employeeInsert, employeeUpdate) = await _employeeSeeder.InitializeEmployeeAsync();
            _logger.Info($"员工数据初始化完成 - 插入: {employeeInsert}, 更新: {employeeUpdate}");

            _logger.Info("HRM数据初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"HRM数据初始化失败: {ex.Message}");
            throw;
        }
    }
}