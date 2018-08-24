# dnd_initiative_tracker
**PHASE 1: INITIAL CONSOLE PROTOTYPE (DONE!)**
## LOOP 1 / LOOP 2 / FINAL OUTPUT

DM adds player name
DM adds player initiative roll
DM adds monster name
DM adds monster initiative roll
Characters all get added to a list, in order of roll
DM screen shows turn order list

**PHASE 2: APPLY OOP PRINCIPLES AND DATABASE**

Rebuild and redifine classes
Add DAL layer and SQL database to save characters
Add DAL Methods
    1. PCs
        a. Create Character DONE!
        b. Get Characters by player id DONE!
        c. Get Character by character id (optional)
    2. Enemies
        a. Add Enemy
        b. Search Enemies by Type DONE!
        c. Search Enemies by CR (optional)
        d. Load in premade encounter
    3. Player
        a. Add player DONE!
        b. Get player DONE!
Add Integration Tests for DAL Methods
Update CLI to utilize new DAL methods DONE BUT NEEDS SOME TWEAKS!
Add Integration Tests for DAL Methods

**PHASE 3: UPLOAD DATABASE TO CLOUD**
Purchase Domain DONE!
Create Cloud Account (probably AWS)
Host Server
Figure out how to log in to server from SQL
Add connection string to Console Project
TEST

**PHASE 4: CONVERT CLI TO MVC**
Set up Controllers and Views
    - DM inputs only, but viewable by all players
Upload to Domain
Connect to Server
TEST

**PHASE 5: ALLOW PLAYER INPUTS FOR WEB APP**
Figure out how to do this lol

**PHASE X: CONVERT FROM INITIATIVE TRACKER TO TURN TRACKER**
NOTE: This phase could be added as early as PHASE 2.5
Update Database with ALL stats and skills
Add turn tracker 
Add attack/spell methods to characters


**OTHER THINGS THAT CAN BE IMPLEMENTED WHENEVS**
Add Dicebag class/methods to business logic
Figure out how to bribe people into doing enemy data entry
