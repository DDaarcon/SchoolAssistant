﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsOrganization;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class OrganizationalClassConfiguration : SemesterDbEntityConfiguration<OrganizationalClass>
    {
        public override void Configure(EntityTypeBuilder<OrganizationalClass> builder)
        {
            base.Configure(builder);
        }
    }
}
