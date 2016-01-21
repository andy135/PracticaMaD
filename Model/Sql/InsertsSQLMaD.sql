USE mad;
DECLARE @Date DateTime = '2016-25-02 11:00:05.000'
BEGIN

INSERT INTO Category(categoryName) VALUES ('Futbol');
INSERT INTO Category(categoryName) VALUES ('Baloncesto');
INSERT INTO Category(categoryName) VALUES ('Remo');
INSERT INTO Category(categoryName) VALUES ('General');


INSERT INTO Event(eventName,review,date,categoryId) VALUES
('BCN - RMA','Partido de fútbol legendario',@Date,(select [categoryId] from Category where categoryName = 'Futbol')),
('Campeonato De Remo','Proximamente',@Date,(select [categoryId] from Category where categoryName = 'Remo')),
('Basket world','Proximamente',@Date,(select [categoryId] from Category where categoryName = 'Baloncesto')),
('USA - ESP','Partido',@Date,(select [categoryId] from Category where categoryName = 'Futbol')),
('Olimpiadas 2016','Proximamente',@Date,(select [categoryId] from Category where categoryName = 'General')),
('NBA Finals','La final',@Date,(select [categoryId] from Category where categoryName = 'Baloncesto')),
('Remo Galicia','Nuevo',@Date,(select [categoryId] from Category where categoryName = 'Remo')),
('Olimpiadas 2020','Proximamente',@Date,(select [categoryId] from Category where categoryName = 'General')),
('Campeonato Europeo Basket','La final',@Date,(select [categoryId] from Category where categoryName = 'Baloncesto')),
('Remo Spain','Nuevo',@Date,(select [categoryId] from Category where categoryName = 'Remo')),
('General motors','Proximamente',@Date,(select [categoryId] from Category where categoryName = 'General'));

END