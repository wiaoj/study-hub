> Now that Git is hopefully installed/ itis time to configure
some basic information. YOU do not need to register for
an account or anythingı but YOU Will need to provide:<br>
>
> - Your `name` & `email` <br>
> 
> if you are using a GUI, it should prompt you for your
name and email the first time you open the app

To configure the name that Git Will
associate with your world run this command:

```properties
git config --global user.name "wiaoj"
```

Do the same thing for your email using the following
command. When we get to Github, you"ll want your
Git email address to match your Github account

```properties
git config --global user.email "wiaoj@git.com"
```

# Repository

A Git `Repo` is a workspace which tracks and
manages files within a folder.
Anytime we want to Use Git with a project, app,
etc we need to create a new git repository. We
can have as many repos on OUr machine as
needed, all with separate histories and contents

## First Git Command!

`git status` gives information on the current
status of a git repository and its contents
itis very useful, but at the moment we don't
actually have any repos to check the status of!

```properties
git status
```

[Git Status Document](https://git-scm.com/docs/git-status)

## Actual First Git Command!

Use `git init` to create a new git repository.
Before we can do anything git-related, we
must initialize a repo first!
This is something you do once per project.
Initialize the repo in the top-level folder
containing your project

```properties
git init
```

[Git Init Document](https://git-scm.com/docs/git-init)

# Committing 

![workflow-image](./images/git-workflow.png)
![workflow-image2](./images/git-workflow-2.png)

## Adding

Use `git add` to add specific files to the staging
area. Separate files with spaces to add
multiple at once

```properties
git add file1 file2
```

> Use `git add .` to stage all changes at one

```properties
git add .
```

[Git Add Document](https://git-scm.com/docs/git-add)

## Git Commit

We use the `git commit` command to actually
commit changes from the staging area.
When makine a commit, we need to provide a
commit message that summarizes the changes
and work snapshotted in the commit

> Running `git commit` will commit all staged
changes. It also opens up a text editor and
prompts you for a commit message.
This can be overwhelming when you're starting
out, so instead you can use...

```properties
git commit
```

> The -m flag allows us to pass in an inline
commit message, rather than launching a text
editor.
We'll learn more about writing good commit
messages later on.

```properties
git commit -m "my message"
```

[Git Commit Document](https://git-scm.com/docs/git-commit)

# Git Log

```properties
git log
```

[Git Log Document](https://git-scm.com/docs/git-log)

- [Section Slide](./slides/Git+&+Github_+Basics.pdf) 
- [Section Slide Online](https://www.canva.com/design/DAEPH_Lq4Wk/wGpxJyUvCHfQT3jeXcs-Ww/view?utm_content=DAEPH_Lq4Wk&utm_campaign=designshare&utm_medium=link&utm_source=viewer)

## Atomic Commits
> When possible, a commit should encompass a
sinele feature, change, or fix. In other words, try
to keep each commit focused on a single thing.
This makes it much easier to Undo or rollback
changes later on. It also makes your code or
project easier to review.

## Writing Commit Message

Present or past tense? Does it really matter?

> Describe your changes in imperative mood, e.g.
"make xyzzy do frotz" instead of "[This patch]
makes xyzzy do frotz" or "I changed xyzzy to do
frotz", as if you are giving orders to the
codebase to change its behavior.

> You do NOT have to follow this pattern

Though the Git docs suggest Using present-tense
imperative messages, many developers prefer to
Use past-tense messages. All that matters is
consistency, especially when working on a team
with many people making commits

## Amending Commits

Suppose you just made a commit and then realized
you forgot to include a file! Or, maybe you made a
typo in the commit message that you want to
correct.
Rather than making a brand new separate commit,
YOU can "redo" the previous commit using
the `--amend` option

```properties
git commit -m "some commit"
git add forgotten_file
git commit --amend
```

[Git Commit Document](https://git-scm.com/docs/git-commit)

## Ignoring Files

We can tell Git which files and directories to
ignore in a given repository, Using a `.gitignore` file.
This is useful for files you know you **NEVER** want to
commit, including:

- Secretsı API keys, credentialsı etc.
- Operating System files (.DS_Store on Mac)
- Log files
- Dependencies 8 packages


### .gitignore
Create a file called `.gitignore` in the root of a
repository. Inside the file, we can write patterns to
tell Git which files 8 folders to ignore:
- .DS_Store Will ignore files named .DS_Store
- folderName/ Will ignore an entire directory
- *.log Will ignore any files with the .log extension

- [Section Slide](./slides/Git+&+Github_+Committing+In+Detail.pdf) 
- [Section Slide Online](https://www.canva.com/design/DAEXMibkysc/uO4KEygGGLG13bkHSnzbgQ/view)