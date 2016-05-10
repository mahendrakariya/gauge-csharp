# Copyright 2015 ThoughtWorks, Inc.

# This file is part of Gauge-CSharp.

# Gauge-CSharp is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.

# Gauge-CSharp is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.

# You should have received a copy of the GNU General Public License
# along with Gauge-CSharp.  If not, see <http://www.gnu.org/licenses/>.

param([string]$artifactPath='',[string]$force="false",[string]$gauge="gauge.exe")

if ($artifactPath -ne '') {
    $artifactPath=resolve-path $artifactPath
}
else {
    $artifactPath= [IO.Path]::Combine($pwd,"artifacts")
}

$pluginFile = gci $artifactPath\gauge-csharp*.zip | select -f 1

if($force -eq 'true')
{
    &$gauge --uninstall csharp
}

Write-Host "Installing from file: " $pluginFile
&$gauge --install csharp -f $pluginFile
