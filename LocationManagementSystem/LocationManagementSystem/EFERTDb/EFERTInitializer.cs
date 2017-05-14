using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Utilities;
using System.IO;
using System.Xml;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure;

namespace LocationManagementSystem
{
    public class EFERTInitializer : DropCreateDatabaseIfModelChanges<EFERTDbContext>
    {
        protected override void Seed(EFERTDbContext context)
        {
            #region Plant Locations

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "SECURITY BUILDING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MARKETING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "ADMIN BUILDING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "WORKSHOP",
                IsOnPlant = true
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "SAP BUILDING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "WAREHOUSE",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "TECHNICAL BUILDING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "LABORATORY",
                IsOnPlant = true
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "ENVEN BUILDING",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "PM&S-1",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "PM&S-II",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "PM&S-III",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "UREA-1",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "UREA-II",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "AMMONIA-II",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "CCR",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "UREA-II MAINTENANCE",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "CLIENT WAREHOUSE",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "TRAINING BUILDING",
                IsOnPlant = true
            });

            #endregion

            #region Colony Location

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "NEW MARKET",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "OLD MARKET",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT CLUB",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "GRAMMAR SCHOOL",
                IsOnPlant = false
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT LAUNDRY",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT CRICKET STADIUM",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT SCHOOL GROUND",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT GUEST HOUSE",
                IsOnPlant = false
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT BOQ-1",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT BOQ-II",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT BOQ-III",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT BOQ-IV",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MANAGEMENT MARIT SWEET",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "ENGRO COLLAGE",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "MASJID",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES LAUNDRY",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES CLUB",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "OLD EMPLOYEES STADIUM",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "NEW EMPLOYEES STADIUM",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES COMMUNITY GROUND",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES ETP",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES GUEST HOUSE",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES BOQ-1",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES BOQ-II",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES X-BUILDING",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "EMPLOYEES DOUBLE STORY BOQ",
                IsOnPlant = false
            });

            #endregion

            #region Departments

            context.Departments.Add(new DepartmentInfo()
            {
                DepartmentName = "ADMINISTRATION"
            });

            context.Departments.Add(new DepartmentInfo()
            {
                DepartmentName = "HSE&T"
            });

            context.Departments.Add(new DepartmentInfo()
            {
                DepartmentName = "I&E"
            });

            #endregion

            #region Sections

            context.Sections.Add(new SectionInfo()
            {
                SectionName = "ADMINISTRATION"
            });

            context.Sections.Add(new SectionInfo()
            {
                SectionName = "HSE&T"
            });

            context.Sections.Add(new SectionInfo()
            {
                SectionName = "I&E"
            });

            #endregion

            #region Company

            context.Companies.Add(new CompanyInfo()
            {
                CompanyName = "ADMINISTRATION"
            });

            context.Companies.Add(new CompanyInfo()
            {
                CompanyName = "HSE&T"
            });

            context.Companies.Add(new CompanyInfo()
            {
                CompanyName = "I&E"
            });

            #endregion

            #region Designation

            context.Designations.Add(new DesignationInfo()
            {
                Designation = "ADMINISTRATION"
            });

            context.Designations.Add(new DesignationInfo()
            {
                Designation = "HSE&T"
            });

            context.Designations.Add(new DesignationInfo()
            {
                Designation = "I&E"
            });

            #endregion

            #region Cadre

            context.Cadres.Add(new CadreInfo()
            {
                CadreName = "ADMINISTRATION"
            });

            context.Cadres.Add(new CadreInfo()
            {
                CadreName = "HSE&T"
            });

            context.Cadres.Add(new CadreInfo()
            {
                CadreName = "I&E"
            });

            #endregion

            base.Seed(context);
        }
    }
}
