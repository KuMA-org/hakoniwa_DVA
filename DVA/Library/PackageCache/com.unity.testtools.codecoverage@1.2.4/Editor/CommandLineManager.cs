using System;
using UnityEditor.TestTools.CodeCoverage.CommandLineParser;
using UnityEditor.TestTools.CodeCoverage.Utils;
using UnityEngine;

namespace UnityEditor.TestTools.CodeCoverage
{
    internal class CommandLineManager : CommandLineManagerImplementation
    {
        private static CommandLineManager s_Instance = null;

        public static CommandLineManager instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new CommandLineManager();

                return s_Instance;
            }
        }

        protected CommandLineManager() : base(Environment.GetCommandLineArgs())
        {
        }
    }

    internal class CommandLineManagerImplementation
    {
        public bool runFromCommandLine
        {
            get;
            private set;
        }

        public string coverageResultsPath
        {
            get;
            private set;
        }

        public string coverageHistoryPath
        {
            get;
            private set;
        }

        public bool generateAdditionalMetrics
        {
            get;
            private set;
        }

        public bool generateHTMLReportHistory
        {
            get;
            private set;
        }

        public bool generateHTMLReport
        {
            get;
            private set;
        }

        public bool generateBadgeReport
        {
            get;
            private set;
        }

        public bool assemblyFiltersSpecified
        {
            get;
            private set;
        }

        public bool assemblyFiltersFromFileSpecified
        {
            get;
            private set;
        }

        public bool pathFiltersSpecified
        {
            get;
            private set;
        }

        public AssemblyFiltering assemblyFiltering
        {
            get;
            private set;
        }

        public PathFiltering pathFiltering
        {
            get;
            private set;
        }

        public bool runTests
        {
            get;
            private set;
        }

        public bool batchmode
        {
            get;
            private set;
        }

        public bool burstDisabled
        {
            get;
            private set;
        }

        private string m_CoverageOptionsArg;
        private string[] m_CoverageOptions;
        private string m_IncludeAssemblies;
        private string m_ExcludeAssemblies;
        private string m_IncludePaths;
        private string m_ExcludePaths;

        public CommandLineManagerImplementation(string[] commandLineArgs)
        {
            runFromCommandLine = false;
            coverageResultsPath = string.Empty;
            coverageHistoryPath = string.Empty;
            generateAdditionalMetrics = false;
            generateHTMLReportHistory = false;
            generateHTMLReport = false;
            generateBadgeReport = false;
            assemblyFiltersSpecified = false;
            pathFiltersSpecified = false;
            assemblyFiltering = new AssemblyFiltering();
            pathFiltering = new PathFiltering();
            runTests = false;
            batchmode = false;
            burstDisabled = false;

            m_CoverageOptionsArg = string.Empty;
            m_CoverageOptions = new string[] { };
            m_IncludeAssemblies = string.Empty;
            m_ExcludeAssemblies = string.Empty;
            m_IncludePaths = string.Empty;
            m_ExcludePaths = string.Empty;

            CommandLineOptionSet optionSet = new CommandLineOptionSet(
                new CommandLineOption("enableCodeCoverage", () => { runFromCommandLine = true; }),
                new CommandLineOption("coverageResultsPath", filePathArg => { SetCoverageResultsPath(filePathArg); }),
                new CommandLineOption("coverageHistoryPath", filePathArg => { SetCoverageHistoryPath(filePathArg); }),
                new CommandLineOption("coverageOptions", optionsArg => { AddCoverageOptions(optionsArg); }),
                new CommandLineOption("runTests", () => { runTests = true; }),
                new CommandLineOption("batchmode", () => { batchmode = true; }),
                new CommandLineOption("-burst-disable-compilation", () => { burstDisabled = true; })
            );
            optionSet.Parse(commandLineArgs);

            ValidateCoverageResultsPath();
            ValidateCoverageHistoryPath();

            if (runFromCommandLine)
                ParseCoverageOptions();
        }

        private void SetCoverageResultsPath(string filePathArg)
        {
            if (coverageResultsPath != string.Empty)
            {
                ResultsLogger.Log(ResultID.Warning_MultipleResultsPaths, coverageResultsPath);
            }
            else
            {
                if (filePathArg != null)
                {
                    coverageResultsPath = CoverageUtils.NormaliseFolderSeparators(filePathArg);
                }
            }
        }

        private void ValidateCoverageResultsPath()
        {
            if (!CoverageUtils.EnsureFolderExists(coverageResultsPath))
                coverageResultsPath = string.Empty;
        }

        private void SetCoverageHistoryPath(string filePathArg)
        {
            if (coverageHistoryPath != string.Empty)
            {
                ResultsLogger.Log(ResultID.Warning_MultipleHistoryPaths, coverageHistoryPath);
            }
            else
            {
                if (filePathArg != null)
                {
                    coverageHistoryPath = CoverageUtils.NormaliseFolderSeparators(filePathArg);
                }
            }
        }

        private void ValidateCoverageHistoryPath()
        {
            if (!CoverageUtils.EnsureFolderExists(coverageHistoryPath))
                coverageHistoryPath = string.Empty;
        }

        private void AddCoverageOptions(string coverageOptionsArg)
        {
            if (coverageOptionsArg != null)
            {
                coverageOptionsArg = coverageOptionsArg.Trim('\'');

                if (coverageOptionsArg != string.Empty)
                {
                    if (m_CoverageOptionsArg == string.Empty)
                    {
                        m_CoverageOptionsArg = coverageOptionsArg;
                    }
                    else
                    {
                        m_CoverageOptionsArg += ";";
                        m_CoverageOptionsArg += coverageOptionsArg;
                    }
                }
            }
        }

        private void ParseCoverageOptions()
        {
            m_CoverageOptions = m_CoverageOptionsArg.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string optionArgsStr in m_CoverageOptions)
            {
                if (optionArgsStr.Length == 0)
                    continue;

                string optionName = optionArgsStr;
                string optionArgs = string.Empty;

                int indexOfColon = optionArgsStr.IndexOf(':');
                if (indexOfColon > 0)
                {
                    optionName = optionArgsStr.Substring(0, indexOfColon);
                    optionArgs = optionArgsStr.Substring(indexOfColon+1);
                }        

                switch (optionName.ToUpperInvariant())
                {
                    case "GENERATEADDITIONALMETRICS":
                        generateAdditionalMetrics = true;
                        break;

                    case "GENERATEHTMLREPORTHISTORY":
                        generateHTMLReportHistory = true;
                        break;

                    case "GENERATEHTMLREPORT":
                        generateHTMLReport = true;
                        break;

                    case "GENERATEBADGEREPORT":
                        generateBadgeReport = true;
                        break;

                    case "VERBOSITY":
                        if (optionArgs.Length > 0)
                        {
                            switch (optionArgs.ToUpperInvariant())
                            {
                                case "VERBOSE":
                                    ResultsLogger.VerbosityLevel = LogVerbosityLevel.Verbose;
                                    break;
                                case "INFO":
                                    ResultsLogger.VerbosityLevel = LogVerbosityLevel.Info;
                                    break;
                                case "WARNING":
                                    ResultsLogger.VerbosityLevel = LogVerbosityLevel.Warning;
                                    break;
                                case "ERROR":
                                    ResultsLogger.VerbosityLevel = LogVerbosityLevel.Error;
                                    break;
                                case "OFF":
                                    ResultsLogger.VerbosityLevel = LogVerbosityLevel.Off;
                                    break;
                            }
                        }
                        break;

                    case "ASSEMBLYFILTERS":
                        if (optionArgs.Length > 0)
                        {
                            assemblyFiltersSpecified = true;

                            string[] assemblyFilters = optionArgs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

<<<<<<<< HEAD:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.0.1/Editor/CommandLineManager.cs
                            for (int i = 0; i < assemblyFilters.Length; ++i)
                            {
                                string filter = assemblyFilters[i];
                                string filterBody = filter.Length > 1 ? filter.Substring(1) : string.Empty;

                                if (filter.StartsWith("+", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (m_IncludeAssemblies.Length > 0)
                                        m_IncludeAssemblies += ",";

                                    if (filterBody.StartsWith("<", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (string.Equals(filterBody, AssemblyFiltering.kUserAlias, StringComparison.OrdinalIgnoreCase))
                                            m_IncludeAssemblies += AssemblyFiltering.GetUserOnlyAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kProjectAlias, StringComparison.OrdinalIgnoreCase))
                                            m_IncludeAssemblies += AssemblyFiltering.GetAllProjectAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kPackagesAlias, StringComparison.OrdinalIgnoreCase))
                                            m_IncludeAssemblies += AssemblyFiltering.GetPackagesOnlyAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kAllAlias, StringComparison.OrdinalIgnoreCase))
                                            m_IncludeAssemblies += "*";
                                    }
                                    else
                                    {
                                        m_IncludeAssemblies += filterBody;
                                    }
                                }
                                else if (filter.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (m_ExcludeAssemblies.Length > 0)
                                        m_ExcludeAssemblies += ",";

                                    if (filterBody.StartsWith("<", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (string.Equals(filterBody, AssemblyFiltering.kUserAlias, StringComparison.OrdinalIgnoreCase))
                                            m_ExcludeAssemblies += AssemblyFiltering.GetUserOnlyAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kProjectAlias, StringComparison.OrdinalIgnoreCase))
                                            m_ExcludeAssemblies += AssemblyFiltering.GetAllProjectAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kPackagesAlias, StringComparison.OrdinalIgnoreCase))
                                            m_ExcludeAssemblies += AssemblyFiltering.GetPackagesOnlyAssembliesString();
                                        else if (string.Equals(filterBody, AssemblyFiltering.kAllAlias, StringComparison.OrdinalIgnoreCase))
                                            m_ExcludeAssemblies += "*";
                                    }
                                    else
                                    {
                                        m_ExcludeAssemblies += filterBody;
                                    }
                                }
                                else
                                {
                                    ResultsLogger.Log(ResultID.Warning_AssemblyFiltersNotPrefixed, filter);
                                }
                            }
                        }
                        break;

========
                            ParseAssemblyFilters(assemblyFilters);
                        }
                        break;

                    case "PATHFILTERSFROMFILE":
                        if (optionArgs.Length > 0)
                        {
                            pathFiltersFromFileSpecified = true;
                            ResultsLogger.Log(ResultID.Warning_PathFiltersFromFileDeprecation);

                            if (File.Exists(optionArgs))
                            {
                                try
                                {
                                    ParsePathFilters( GetPathFiltersFromFile(optionArgs) );
                                }
                                catch (Exception e)
                                {
                                    ResultsLogger.Log(ResultID.Warning_FailedToExtractPathFiltersFromFile, e.Message, optionArgs);
                                }
                            }
                        }
                        break;

>>>>>>>> main:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.2.4/Editor/CommandLineManager.cs
                    case "PATHFILTERS":
                        if (optionArgs.Length > 0)
                        {
                            pathFiltersSpecified = true;

                            string[] pathFilters = optionArgs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

<<<<<<<< HEAD:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.0.1/Editor/CommandLineManager.cs
                            for (int i = 0; i < pathFilters.Length; ++i)
========
                            ParsePathFilters(pathFilters);
                        }
                        break;

                    case "FILTERSFROMFILE":
                        if (optionArgs.Length > 0)
                        {
                            try
                            {
                                JsonFile jsonFile = GetFiltersFromFile(optionArgs);
                                if (jsonFile != null)
                                {
                                    string[] pathFilters = ConvertToFilterArray(jsonFile.pathsInclude, jsonFile.pathsExclude);
                                    if (pathFilters != null && pathFilters.Length > 0)
                                    {
                                        pathFiltersFromFileSpecified = true;
                                        ParsePathFilters(pathFilters);
                                    }

                                    string[] assemblyFilters = ConvertToFilterArray(jsonFile.assembliesInclude, jsonFile.assembliesExclude);
                                    if (assemblyFilters != null && assemblyFilters.Length > 0)
                                    {
                                        assemblyFiltersFromFileSpecified = true;
                                        ParseAssemblyFilters(assemblyFilters);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                ResultsLogger.Log(ResultID.Warning_FailedToExtractFiltersFromFile, e.Message, optionArgs);
                            }
                        }
                            break;
                        

                    case "PATHREPLACEPATTERNS":
                        if (optionArgs.Length > 0)
                        {
                            pathReplacingSpecified = true;
                            m_PathReplacePatterns = optionArgs;
                        }
                        break;

                    case "SOURCEPATHS":
                        if (optionArgs.Length > 0)
                        {
                            string[] rawSourcePaths = optionArgs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < rawSourcePaths.Length; ++i)
>>>>>>>> main:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.2.4/Editor/CommandLineManager.cs
                            {
                                string filter = pathFilters[i];
                                string filterBody = filter.Length > 1 ? filter.Substring(1) : string.Empty;

                                if (filter.StartsWith("+", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (m_IncludePaths.Length > 0)
                                        m_IncludePaths += ",";
                                    m_IncludePaths += filterBody;
                                }
                                else if (filter.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (m_ExcludePaths.Length > 0)
                                        m_ExcludePaths += ",";
                                    m_ExcludePaths += filterBody;
                                }
                                else
                                {
                                    ResultsLogger.Log(ResultID.Warning_PathFiltersNotPrefixed, filter);
                                }
                            }
                        }
                        break;
                }
            }

            if (m_IncludeAssemblies.Length == 0)
            {
                // If there are no inlcudedAssemblies specified but there are includedPaths specified
                // then include all project assemblies so path filtering can take precedence over assembly filtering,
                // othewise if there are no includedPaths specified neither then inlcude just the user assemblies (found under the Assets folder)
                
                if (m_IncludePaths.Length > 0)
                    m_IncludeAssemblies = AssemblyFiltering.GetAllProjectAssembliesString();
                else
                    m_IncludeAssemblies = AssemblyFiltering.GetUserOnlyAssembliesString();
            }

            if (m_ExcludeAssemblies.Length > 0)
                m_ExcludeAssemblies += ",";

            m_ExcludeAssemblies += AssemblyFiltering.kDefaultExcludedAssemblies;

            assemblyFiltering.Parse(m_IncludeAssemblies, m_ExcludeAssemblies);
            pathFiltering.Parse(m_IncludePaths, m_ExcludePaths);
<<<<<<<< HEAD:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.0.1/Editor/CommandLineManager.cs
========
            pathReplacing.Parse(m_PathReplacePatterns);
        }

        private void ParseAssemblyFilters(string[] assemblyFilters)
        {
            for (int i = 0; i < assemblyFilters.Length; ++i)
            {
                string filter = assemblyFilters[i];
                string filterBody = filter.Length > 1 ? filter.Substring(1) : string.Empty;

                if (filter.StartsWith("+", StringComparison.OrdinalIgnoreCase))
                {
                    if (m_IncludeAssemblies.Length > 0)
                        m_IncludeAssemblies += ",";

                    if (filterBody.StartsWith("<", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(filterBody, AssemblyFiltering.kAssetsAlias, StringComparison.OrdinalIgnoreCase))
                            m_IncludeAssemblies += AssemblyFiltering.GetUserOnlyAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kAllAlias, StringComparison.OrdinalIgnoreCase))
                            m_IncludeAssemblies += AssemblyFiltering.GetAllProjectAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kPackagesAlias, StringComparison.OrdinalIgnoreCase))
                            m_IncludeAssemblies += AssemblyFiltering.GetPackagesOnlyAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kCoreAlias, StringComparison.OrdinalIgnoreCase))
                            m_IncludeAssemblies += AssemblyFiltering.kCoreAssemblies;
                    }
                    else
                    {
                        m_IncludeAssemblies += filterBody;
                    }
                }
                else if (filter.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                {
                    if (m_ExcludeAssemblies.Length > 0)
                        m_ExcludeAssemblies += ",";

                    if (filterBody.StartsWith("<", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(filterBody, AssemblyFiltering.kAssetsAlias, StringComparison.OrdinalIgnoreCase))
                            m_ExcludeAssemblies += AssemblyFiltering.GetUserOnlyAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kAllAlias, StringComparison.OrdinalIgnoreCase))
                            m_ExcludeAssemblies += AssemblyFiltering.GetAllProjectAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kPackagesAlias, StringComparison.OrdinalIgnoreCase))
                            m_ExcludeAssemblies += AssemblyFiltering.GetPackagesOnlyAssembliesString();
                        else if (string.Equals(filterBody, AssemblyFiltering.kCoreAlias, StringComparison.OrdinalIgnoreCase))
                            m_ExcludeAssemblies += AssemblyFiltering.kCoreAssemblies;
                    }
                    else
                    {
                        m_ExcludeAssemblies += filterBody;
                    }
                }
                else
                {
                    ResultsLogger.Log(ResultID.Warning_AssemblyFiltersNotPrefixed, filter);
                }
            }
        }

        private void ParsePathFilters(string[] pathFilters)
        {
            var sources = new string[0];
            if (sourcePathsSpecified)
                sources = sourcePaths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pathFilters.Length; ++i)
            {
                string filter = pathFilters[i];
                string filterBody = filter.Length > 1 ? filter.Substring(1) : string.Empty;

                var isRelative = !filterBody.StartsWith("*") && !filterBody.StartsWith("?") && string.IsNullOrEmpty(Path.GetPathRoot(filterBody));
                //If current path is relative - expand it to an absolute path using specified source paths
                if (isRelative && sourcePathsSpecified)
                {
                    string expandedPaths = string.Empty;
                    foreach (var source in sources)
                    {
                        if (expandedPaths.Length > 0)
                            expandedPaths += ",";
                        expandedPaths += CoverageUtils.NormaliseFolderSeparators(Path.Combine(source, filterBody));
                    }
                    filterBody = expandedPaths;
                }

                if (filter.StartsWith("+", StringComparison.OrdinalIgnoreCase))
                {
                    if (m_IncludePaths.Length > 0)
                        m_IncludePaths += ",";
                    m_IncludePaths += filterBody;
                }
                else if (filter.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                {
                    if (m_ExcludePaths.Length > 0)
                        m_ExcludePaths += ",";
                    m_ExcludePaths += filterBody;
                }
                else
                {
                    ResultsLogger.Log(ResultID.Warning_PathFiltersNotPrefixed, filter);
                }
            }

            if (m_IncludePaths.Length > 0 || m_ExcludePaths.Length > 0)
                pathFiltersSpecified = true;
        }

        internal string[] GetPathFiltersFromFile(string path)
        {
            var paths = new List<string>();

            foreach (var line in File.ReadAllLines(path))
            {
                var entry = line.Trim();
                paths.Add(CoverageUtils.NormaliseFolderSeparators(entry));
            }

            return paths.ToArray();
>>>>>>>> main:DVA/Library/PackageCache/com.unity.testtools.codecoverage@1.2.4/Editor/CommandLineManager.cs
        }

        internal JsonFile GetFiltersFromFile(string path)
        {
            string jsonString = JsonUtils.CleanJsonString(File.ReadAllText(path));
            JsonUtils.ValidateJsonKeys(jsonString);
            JsonFile jsonFile = JsonUtility.FromJson<JsonFile>(jsonString);
            return jsonFile;
        }

        internal string[] ConvertToFilterArray(string[] include, string[] exclude)
        {
            var filtersList = new List<string>();

            if (include != null && include.Length > 0)
            {
                foreach (var filter in include)
                {
                    filtersList.Add($"+{filter}");
                }
            }
            if (exclude != null && exclude.Length > 0)
            {
                foreach (var filter in exclude)
                {
                    filtersList.Add($"-{filter}");
                }
            }
            return filtersList.ToArray();
        }
    }
}
