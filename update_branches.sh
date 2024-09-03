#!/bin/bash

# Fetch all branches from the remote repository and prune deleted ones
git fetch --all --prune

# Ensure the local master branch is up to date
git checkout master
git pull origin master

# Get a list of all active remote branches (excluding master and deleted branches)
branches=$(git branch -r | grep -v '\->' | grep -v 'master' | sed 's/origin\///')

# Loop through each branch and update it with master
for branch in $branches; do
    # Check if the branch exists locally or if it's been deleted remotely
    if git show-ref --quiet refs/heads/$branch; then
        echo "Updating branch: $branch"
        
        # Checkout the branch
        git checkout $branch
        
        # Pull the latest changes for the branch
        git pull origin $branch
        
        # Merge the master branch into the current branch
        git merge master
        
        # Push the updated branch back to the remote repository
        git push origin $branch
    else
        echo "Skipping branch: $branch (it has been deleted locally or remotely)"
    fi
done

# Switch back to the master branch
git checkout master

echo "All active branches have been updated with master."
