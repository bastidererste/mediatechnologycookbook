

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
		$outFolder = 'C:\Hap\'
		# Define the default hap format: 1 hap; 2 hap_alpha; 3 hap_q
		$hapFormat = '2'

		# Add debug line
		Write-Host "Debug: Output file will be `"$outputFile`""

		if ($inputFileName -match 'hap1') {
            $hapFormat = '1'
        }
        elseif ($inputFileName -match 'hap2') {
            $hapFormat = '2'
        }
        elseif ($inputFileName -match 'hap3') {
            $hapFormat = '3'
        }
        else {
            Write-Host "Input filename does not contain 'hap1', 'hap2', or 'hap3'. Using default format."
           
        }

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
				$outputFile = $outFolder + $inputFileName + "_hapq.mov"

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
