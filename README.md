
# MVVM&lt;BareKnuckleStyle&gt;

an

- ultra-leightweight,
- flexible,
- extensible,
- versatile,
- yet scalable utilitybelt
 
for MVVM-based applications, featuring some really cool and never been seen approaches for common problems.

Most of the below described packages can be mixed-and-matched independently in any app, regardless of the perhaps existing MVVM-Framework

##### master-branch
[![Build status (master)](https://ci.appveyor.com/api/projects/status/ef83fo2jh7onio60/branch/master?svg=true)](https://ci.appveyor.com/project/earloc/blue-mvvm/branch/master)

##### dev-branch
[![Build status (dev)](https://ci.appveyor.com/api/projects/status/ef83fo2jh7onio60/branch/dev?svg=true)](https://ci.appveyor.com/project/earloc/blue-mvvm/branch/dev)

## Blue.MVVM
[NuGet](https://www.nuget.org/packages/Blue.MVVM/)


## Blue.MVVM.Commands
[NuGet](https://www.nuget.org/packages/Blue.MVVM.Commands/)

## Blue.MVVM.IoC
[NuGet](https://www.nuget.org/packages/Blue.MVVM.IoC/)

## Blue.MVVM.Navigation
[NuGet](https://www.nuget.org/packages/Blue.MVVM.Navigation/)

# Why another MVVM framework ???

Blue.MVVM is not a framework. It is a set of common needed types supporting MVVM-based applications.

Most full featured frameworks come with a vast set of types / patterns built into them, at the cost of a steep learning-curve in addition to the hurdles adopting MVVM.

As one learns to adopt the MVVM pattern, most of the time (at least at the beginning), there is e.g. no need for a fully featured DI-Container, super-decoupled messaging-system, fancy-pancy-fluid-interface-factory-builder-converter-abstraction-implementations, etc. .
Also, many of this (surely well thought) built in features often lack the possibility to simply switch e.g. the DI-container with an implementation that better suits the app / the team (regardless of their open-source-status).

Blue.MVVM does not feauture a fully integrated experience when developing your app, it just kicks in when needed, working as expected, fists ready to kick some bug-butts - bareknuckle-style.
