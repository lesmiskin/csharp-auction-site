-- initial cleanup to permit re-running
drop table if exists auctions;

CREATE TABLE auctions (
	id integer PRIMARY KEY,
	title text NOT NULL
);

insert into auctions(id, title) values(1, 'test auction');

select * from auctions;