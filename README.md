# LadySnake-2.0

Course Project for 526.

Related Link: [Learn Git](https://www.atlassian.com/git/tutorials/syncing) / [Linux Command](http://www.informit.com/blogs/blog.aspx?uk=The-10-Most-Important-Linux-Commands)

---

### Standard for coding

> Make sure Git command can be executed in your terminal.

#### Steps to set up your local LadySnake:

1. Open Terminal, go to the path you would like to store this project use `cd` command;
2. Do `git clone https://github.com/yinghuihao/LadySnake-2.0.git`, this will clone the project from Github to your local machine;
3. Go into the /LadySnake folder, check your branch is on *master*: `git branch`.

> Branch is like different version of the code. We normally use *master* branch as the major and no-bug version code base for demo/present, and *devel-qa* as the testing and quality assurance branch for team leader/code reviewer to merge code from different members to do the test.

For different team member, you should have your own branch:

| Member  | Branch |
|---------|--------|
| Yinghui Hao | [haoyinghui](https://github.com/yinghuihao/LadySnake-2.0/tree/haoyinghui)|
| Boyang Yu | [boyangyu](https://github.com/yinghuihao/LadySnake-2.0/tree/boyangyu) |
| Ruoyi Jia | [ruoyijia](https://github.com/yinghuihao/LadySnake-2.0/tree/ruoyijia) |
| Ying Liu | [yingliu](https://github.com/yinghuihao/LadySnake-2.0/tree/yingliu)|
| Buoyan T |[buoyant](https://github.com/yinghuihao/LadySnake-2.0/tree/boyant)|

#### Steps to set up your own Branch:

1. In the /LadySnake folder, do `git checkout -b <your new branch name> <from branch name>` to create your own local new branch (For here, <from branch name> can be *master*).
2. Then do `git push origin <your new branch name>` to push your branch to Github. And now you should be able to work on your own branch. You can check your current branch with `git branch`.

#### Standards of coding every week:

1. Before every week's task, do `git pull` to sync your local folder with remote Github;
2. Then do `git merge master` to sync your own branch with the lateset all-merged, no-bug version of project. If there are any merge conflicts, please go and see instructions in Fix Conflicts Section;
3. After a few changes of the code (usually everyday or two days), do `git add .`, and `git commit -m "<your code>"` to commit the changes on the current branch, then do a `git push origin <your branch name>` to push your changes to Github (It is **RECOMMEND** for you to do a `git pull` before you push to Github);
4. After you test your code and feels it meets the requirements, remember to have your latest code on Github before STEP 5;
5. Then go to Github, Find the `Pull requests` tab on the top and create a new pull request. The base branch should be `devel-qa`, and the compare branch should be your own branch. If it says able to merge, click on "Create Pull Request" and go to next step. Otherwise you have to follow the instructions in Fix Conflicts Section;
6. Change the title and comments to whatever you think is necessary for team leader/code reviewer to merge and test your code. On the right side you can add reviewers/labels if you like, then click on "Create Pull Request", notify your team leader/code reviewer and wait for further feedbacks.

> `git status` can check your code status and tell you if you need to `git pull` or `add` or `commit`.
> **Note:** If you already have your open pull request, there is no need to create a new one, for every changes you make as long as you push your latest code to Github, the opened pull request with same base and compare branch will automatically have your new changes.

#### Standards for team leader/code reviewer:

1. After your team member created the pull request, do `git pull` to sync all code from Github;
2. If you are not on *devel-qa* branch(`git branch`), do `git status` to see if you need to commit on recent code first. Then do `git checkout devel-qa` to go to *devel-qa* branch;
3. Do `git merge origin/<ready to test branch name>` to merge the branch. If you see any merge conflicts, tell your member to fix and re-push the fixed branch, then you follow the steps from STEP 1. With no merge conflicts, go to next step;
4. Open Unity to test the code, try all cases you can imagine of around this task. If it passes, go to Github -> Pull Request tab -> Click on "Merge Pull Request" button. This will merge this task feature with former well operated code on *devel-qa*;
5. After all of the members finish their task and you finish the testing on *devel-qa* branch, your local and remote *devel-qa* branch should be the latest no bug version code base with all new features. Create pull request with base branch *master* and compare branch *devel-qa*. After merging this pull request, your team is ready for next week's tasks.

> **NOTE:** Before you merge the *devel-qa* branch to *master* branch, make sure your *devel-qa* branch is the branch that has all features merged from tasks your team have this week with no bugs. If someone fails to finish his task on time, do not merge his branch to *devel-qa* branch and further to *master* branch. Your *master* branch should always be a no-bug version code base that you can demo/present to others.

> **NOTE:** There may be a time you have your *devel-qa* branch to roll back to previous version(Like someone's code has a bug and while he is fixing you have other member's code to merge and test on *devel-qa* branch), please go and see instructions in Roll Back Section;


---

#### Instructions on Fixing Conflicts:

When you see merge conflicts, you can see which files are having the conflicts from terminal.

Go to these files, then:

1. Find codes between all pairs of "<<<<<<< HEAD", "=======" and ">>>>>>> <other/branch/name>", this indicates where you are having conflicts;
2. The code between "<<<<<<< HEAD" and "=======" is the code on your current branch, and the code between "=======" and ">>>>>>> <other/branch/name>" is the code from merged branch. Decide which one to remain and which one to delete, then remove the parts you don't need as well as the "<<<<<<< HEAD", "=======" and ">>>>>>> <other/branch/name>" rows.
3. After you fix all the conflicts, do `add` and `commit` again to comment on your changes, and `push` to Github.

#### Instructions on Roll Back:

> Git command provide verison control with `git reset`.

1. Do `git log` to check all commits on the current branch, this will include commit id, author, date and commit;
2. Find the one you would like to roll back to, do `git reset --hard/--soft <commit id>`;

> `--hard` will make your local code and local history be just like it was at that commit id. But then if you wanted to push this to someone else who has the new history, it would fail; `--soft` will make your local files changed to be like they were at that commit id, but leave your history as the same.

For complex reset commands, go to this [link](https://git-scm.com/docs/git-reset).

---

If you ever meet any git problems, try google the instructions in terminal and usually you will get the answer.
