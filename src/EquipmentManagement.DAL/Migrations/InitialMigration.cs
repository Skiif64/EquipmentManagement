using FluentMigrator;

namespace EquipmentManagement.DAL.Migrations;

[Migration(20230315144000)]
internal class InitialMigration : Migration
{
    public override void Down()
    {
        Delete.Table("Statuses");
        Delete.Table("Equipments");
        Delete.Table("EquipmentStatuses");
        Delete.Table("Employees");
    }

    public override void Up()
    {
        Create.Table("Employees")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Firstname").AsString()
            .WithColumn("Lastname").AsString()
            .WithColumn("Patronymic").AsString()            
            ;
        Create.Table("Statuses")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString().Unique()
            .WithColumn("Description").AsString()
            ;
        Create.Table("Equipments")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Type").AsString()
            .WithColumn("Article").AsString()
            .WithColumn("SerialNumber").AsInt64()
            .WithColumn("Description").AsString()
            ;
        Create.Table("EquipmentStatuses")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("EmployeeId").AsGuid().Nullable()
            .WithColumn("EquipmentId").AsGuid()
            .WithColumn("StatusId").AsGuid()
            .WithColumn("Modified").AsDateTimeOffset()
            ;
    }
}
