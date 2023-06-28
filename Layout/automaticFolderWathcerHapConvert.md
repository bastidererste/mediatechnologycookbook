
# PowerShell Script for Watching Folder and Converting MOV files

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
| `$outputFolder` | The directory where converted files are saved. Replace `'C:\HapAlpha\'` with the path to your desired output directory. The specified output directory should already exist. | `'C:\HapAlpha\'` |

For example, if you want the script to watch a directory at `D:\MyVideos` and save converted videos to `D:\MyConvertedVideos`, you would set `$folder = 'D:\MyVideos'` and `$outputFolder = 'D:\MyConvertedVideos\'`.

## Notes

The script will automatically check if it is running with administrator privileges. If it is not, it will attempt to restart itself with administrator privileges, prompting the User Account Control (UAC) dialog to request user confirmation.
