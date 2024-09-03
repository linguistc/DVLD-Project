#!/bin/bash

# Fetch all branches from the remote repository
git fetch --all

# Ensure the local master branch is up to date
git checkout master
git pull origin master

# Get a list of all remote branches (excluding master)
branches=$(git branch -r | grep -v '\->' | grep -v 'master' | sed 's/origin\///')

# Loop through each branch and update it with master
for branch in $branches; do
    echo "Updating branch: $branch"
    
    # Checkout the branch
    git checkout $branch
    
    # Pull the latest changes for the branch
    git pull origin $branch
    
    # Merge the master branch into the current branch
    git merge master
    
    # Push the updated branch back to the remote repository
    git push origin $branch
done

# Switch back to the master branch
git checkout master

echo "All branches have been updated with master."
