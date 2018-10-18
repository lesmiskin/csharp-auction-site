-- initial cleanup to permit re-running
drop table if exists auctions;

-- the table structure proper
CREATE TABLE auctions (
	id integer PRIMARY KEY AUTOINCREMENT,
	title text NOT NULL
);

-- example data
insert into auctions(title) values('test auction number 1');
insert into auctions(title) values('test auction number 2');