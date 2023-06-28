
# PowerShell Script for Watching Folder and Converting MOV files
```Powershell
# Get the ID and security principal of the current user account
$myWindowsID = [System.Security.Principal.WindowsIdentity]::GetCurrent()
$myWindowsPrincipal = new-object System.Security.Principal.WindowsPrincipal($myWindowsID)

# Get the security principal for the administrator role
$adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator

# Check to see if we are currently running as an administrator
if ($myWindowsPrincipal.IsInRole($adminRole)) {
    # We are running as an administrator

	$folder =  Split-Path $script:MyInvocation.MyCommand.Path
	Write-Host "Debug: Input folder will be `"$folder`""
	$filter = '*.Mov'            # The type of files you want to watch

    # Create a FileSystemWatcher
    $fsw = New-Object IO.FileSystemWatcher $folder, $filter -Property @{
        IncludeSubdirectories = $false       # Change to $true if you want to watch subdirectories too
        NotifyFilter = [IO.NotifyFilters]'FileName, LastWrite'
    }

    # Define what to do when a file is created
    $onCreated = Register-ObjectEvent $fsw Created -SourceIdentifier FileCreated -Action {
        $path = $Event.SourceEventArgs.FullPath
        Write-Host "A new file '$path' was created."

        # Check if the file is no longer being written to
        $fileReady = $false
        while (-not $fileReady) {
            try {
                [IO.File]::OpenWrite($path).Close()
                $fileReady = $true
            }
            catch {
                Start-Sleep -Milliseconds 500
            }
        }

        # Define the output filename
        # Define the output filename
		$inputFileName = [IO.Path]::GetFileNameWithoutExtension($path)
		$outFolder = 'C:\HapAlpha\'
		# Define the hap format: 1 hap; 2 hap_alpha; 3 hap_q
		$hapFormat = '1'


		# Add debug line
		Write-Host "Debug: Output file will be `"$outputFile`""


		# Execute the command
		
		   switch($hapFormat) {
            '1' { 
				
				$outputFile = $outFolder + $inputFileName + "_hap.mov"

                Start-Process ffmpeg -ArgumentList "-v verbose -y -i `"$path`" -c:v hap `"$outputFile`"" -NoNewWindow -Wait
            }
            '2' {
				$outputFile = $outFolder + $inputFileName + "_hapalpha.mov"

                Start-Process ffmpeg -ArgumentList "-v verbose -y -i `"$path`" -c:v hap -format hap_alpha `"$outputFile`"" -NoNewWindow -Wait
            }
            '3' {
				$outputFile = $outFolder + $inputFileName + "_hap.mov"

                Start-Process ffmpeg -ArgumentList "-v verbose -y -i `"$path`" -c:v hap -format hap_q `"$outputFile`"" -NoNewWindow -Wait
            }
            Default {
                Write-Host "Invalid Format. Coose 1 (hap), 2 (hap_alpha) or 3 (hap_q)"
            }
        }
    }

    # Wait for events
    try {
        while ($true) { Wait-Event -SourceIdentifier FileCreated }
    }
    finally {
        # Clean up the event and FileSystemWatcher when done
        Unregister-Event -SourceIdentifier FileCreated
        $fsw.Dispose()
    }

} else {
    # We are not running as an administrator, so relaunch as administrator

    # Create a new process object that starts PowerShell
    $newProcess = new-object System.Diagnostics.ProcessStartInfo "PowerShell";
   
    # Specify the current script path and name as a parameter with added scope and support for scripts with spaces in it's path
    $newProcess.Arguments = "& '" + $script:MyInvocation.MyCommand.Path + "'"
   
    # Indicate that the process should be elevated
    $newProcess.Verb = "runas";
   
    # Start the new process
    [System.Diagnostics.Process]::Start($newProcess);
   
    # Exit from the current, unelevated, process
    exit
}

```

This PowerShell script watches a specific folder for newly created files with the ".Mov" extension. Once a new MOV file is detected, it uses `ffmpeg` to convert the file into the HAP format with alpha channel. The converted file is saved to a specified output directory.

## Requirements

- `ffmpeg` should be installed and accessible from your PATH environment variable.
- The script needs to be run as an administrator. If it is not started with administrator rights, it will automatically attempt to restart itself as an administrator.

## Usage

1. Save the script as a `.ps1` file.
2. Open PowerShell. The script will automatically run with administrator privileges if needed, prompting User Account Control (UAC) to request user confirmation.
3. Run the script by typing `.\YourScriptName.ps1`, replacing "YourScriptName" with the name you saved your script as OR right click run as powershell script
4. The script will watch the directory where the script itself is located.

## Configuration

You may want to customize some of the variables in the script to suit your needs. The following table lists these variables and what they do:

| Variable | Description | Default Value |
| --- | --- | --- |
| `$folder` | The directory that the script watches for new files. It's set to the directory where the script itself is located. | `Split-Path $script:MyInvocation.MyCommand.Path` |
| `$filter` | The type of files the script watches for. Set this to the file extension you want to monitor. | `'*.Mov'` |
| `$outFolder` | The directory where converted files are saved. Replace `'C:\HapAlpha\'` with the path to your desired output directory. The specified output directory should already exist. | `'C:\HapAlpha\'` |
| `$hapFormat` | The hap codec your files are converted to. Replace `'1'` with the format. Coose 1 (hap), 2 (hap_alpha) or 3 (hap_q)| `'1'` |

For example, if you want the script to watch a directory at `D:\MyVideos` and save converted videos to `D:\MyConvertedVideos`, you would set `$folder = 'D:\MyVideos'` and `$outFolder = 'D:\MyConvertedVideos\'`.

## Notes

The script will automatically check if it is running with administrator privileges. If it is not, it will attempt to restart itself with administrator privileges, prompting the User Account Control (UAC) dialog to request user confirmation.
