-- initial cleanup to permit re-running
drop table if exists auctions;

-- the table structure proper
CREATE TABLE auctions (
	id integer PRIMARY KEY,
	title text NOT NULL
);

-- example data
insert into auctions(id, title) values(1, 'test auction number 1');
insert into auctions(id, title) values(2, 'test auction number 2');