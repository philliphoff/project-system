﻿// Licensed to the .NET Foundation under one or more agreements. The .NET Foundation licenses this file to you under the MIT license. See the LICENSE.md file in the project root for more information.

#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.LanguageServices.ExternalAccess.ProjectSystem.Api;

namespace Microsoft.VisualStudio.ProjectSystem.VS.References
{
    internal class AssemblyReferenceHandler : AbstractReferenceHandler
    {
        internal AssemblyReferenceHandler() :
            base(ProjectSystemReferenceType.Assembly)
        { }

        protected override Task RemoveReferenceAsync(ConfiguredProjectServices services,
            ProjectSystemReferenceInfo referencesInfo)
        {
            Assumes.Present(services.AssemblyReferences);

            AssemblyName? assemblyName = null;
            string? assemblyPath = null;

            if (Path.IsPathRooted((referencesInfo.ItemSpecification)))
            {
                assemblyPath = referencesInfo.ItemSpecification;
            }
            else
            {
                assemblyName = new AssemblyName(referencesInfo.ItemSpecification);
            }

            return services.AssemblyReferences.RemoveAsync(assemblyName, assemblyPath);
        }

        protected override async Task<IEnumerable<IProjectItem>> GetUnresolvedReferencesAsync(ConfiguredProjectServices services)
        {
            Assumes.Present(services.AssemblyReferences);

            return (await services.AssemblyReferences.GetUnresolvedReferencesAsync()).Cast<IProjectItem>();
        }
    }
}
