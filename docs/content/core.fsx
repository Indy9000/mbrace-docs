(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#load "../../packages/MBrace.Azure.Standalone/MBrace.Azure.fsx"
#r "../../packages/MBrace.Flow/lib/net45/MBrace.Flow.dll"


open MBrace
open MBrace.Azure
open MBrace.Azure.Client
open MBrace.Flow


let config = Unchecked.defaultof<Configuration>

(**

# MBrace Technical Overview

MBrace is an open-source distributed runtime that enables
scalable, fault-tolerant computation and data processing for the .NET/mono frameworks.
The MBrace programming model uses a distributed continuation-based approach elegantly
manifested through computation expressions in F#.

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      A prerelease of MBrace.Core can be <a href="https://nuget.org/packages/MBrace.Core">installed from NuGet</a>:
      <pre>PM> Install-Package MBrace.Core -Pre</pre>
    </div>
    <div class="well well-small" id="nuget">
      A prerelease of MBrace.Azure can be <a href="https://nuget.org/packages/MBrace.Azure">installed from NuGet</a>:
      <pre>PM> Install-Package MBrace.Azure -Pre</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

## Example

When using MBrace.Azure, an MBrace session can be initialized from F# interactive as follows:

*)

open MBrace
open MBrace.Azure
open MBrace.Azure.Client

let lineCount () = cloud {
    // enumerate all files from underlying storage container
    let! files = CloudFile.Enumerate "path/to/container"

    // read the contents of a file and return its line count
    let count (f : CloudFile) = cloud {
        let! text = CloudFile.ReadAllText f
        return text.Split('\n').Length
    }
    
    // perform line count in parallel
    let! sizes = files |> Array.map count |> Cloud.Parallel
    return Array.sum sizes
}

// get handle to Azure MBrace cluster
let cluster = Runtime.GetHandle(config)
let proc = cluster.CreateProcess (lineCount ()) // send computation to the runtime
let lines = proc.AwaitResult () // await completion

(**

For a quick dive into MBrace and applications, check out the 
[`MBrace.Demos`](https://github.com/mbraceproject/MBrace.Demos) solution.

    [lang=bash]
    git clone https://github.com/mbraceproject/MBrace.Demos

## Documentation & Tutorials

A collection of tutorials, technical overviews and API references of the library.

  * [Programming Model](programming-model.html) An overview of the MBrace programming model.

  * [Client API](client-api.html) An overview of the MBrace client API.

  * [Cluster Deployment Guide](runtime-deployment.html) Deploying an MBrace runtime.
  
  * [Azure Tutorial](azure-tutorial.html) Getting started with MBrace on Windows Azure.

  * [API Reference](reference/index.html) contains automatically generated documentation for all types, modules
    and functions in the library.

## Community

  * Twitter Feed [@mbracethecloud](https://twitter.com/mbracethecloud).

  * User mailing list [https://groups.google.com/group/mbrace-user](https://groups.google.com/group/mbrace-user).

  * Developer/Contributor mailing list [https://groups.google.com/group/mbrace-dev](https://groups.google.com/group/mbrace-dev).

## Contributing and copyright

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests.

The library is available under the Apache License. 
For more information see the [License file][license] in the GitHub repository. 

  [gh]: https://github.com/mbraceproject/MBrace
  [issues]: https://github.com/mbraceproject/MBrace/issues
  [license]: https://github.com/mbraceproject/MBrace/blob/master/LICENSE.md
*)
