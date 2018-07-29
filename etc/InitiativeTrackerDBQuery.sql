USE [master];
GO


DROP DATABASE [InitiativeTracker];

GO

CREATE DATABASE [InitiativeTracker];
GO

USE [InitiativeTracker];
GO

CREATE TABLE pc (
  pc_id integer identity NOT NULL,
  name varchar(80) NOT NULL,			-- Character Name
  player_id integer NOT NULL,			-- Key of player
  class varchar(80) NOT NULL,			-- Character class
  level integer NOT NULL,				-- Character Level
  initiative_bonus integer NOT NULL,	-- Initiative Bonus
  AC integer NOT NULL,					-- Armor Class
  description varchar(500) NOT NULL,	-- Character Description
  CONSTRAINT pk_pc_pc_id PRIMARY KEY (pc_id)
);

CREATE TABLE npc (
  npc_id integer identity NOT NULL,
  name varchar(80),					   -- Name of the npc
  type varchar(80) NOT NULL,		   -- Name of the npc
  initiative_bonus integer NOT NULL,   -- Initiative Bonus
  AC integer NOT NULL,				   -- Armor Class
  CR decimal NOT NULL,					-- Challenge Rating
  description varchar(500) NOT NULL,   -- Character Description
  CONSTRAINT pk_npc_npc_id PRIMARY KEY (npc_id)
);


CREATE TABLE player (
  player_id integer identity NOT NULL,
  name varchar(80) NOT NULL,		   -- Name of the npc

  CONSTRAINT pk_player_player_id PRIMARY KEY (player_id)
);


-- Player
INSERT INTO player (name)
VALUES ('Jon');

-- PC
INSERT INTO pc (name, player_id, class, level, initiative_bonus, AC, description)
VALUES ('Mirko Youngspear', 1, 'paladin', 7, 2, 16, 'A Dope ass paladin');

-- NPC
INSERT INTO npc( type, initiative_bonus, AC, CR, description)
VALUES ('Goblin (unshielded)', 2, 13, 0.125, 'You know... goblins.');
INSERT INTO npc( type, initiative_bonus, AC, CR, description)
VALUES ('Goblin (shielded)', 2, 15, 0.125, 'You know... goblins.');
INSERT INTO npc( type, initiative_bonus, AC, CR, description)
VALUES ('Bandit', 1, 12, 0.125, 'Bandits rove in gangs and are sometimes led by thugs, veterans, or spellcasters. Not all bandits are evil. Oppression, drought, disease, or famine can often drive otherwise honest folk to a life of banditry.');

ALTER TABLE pc ADD FOREIGN KEY (player_id) REFERENCES player(player_id);


