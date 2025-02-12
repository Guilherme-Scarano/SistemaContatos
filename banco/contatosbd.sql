create database if not exists CadastroContatosDB;

use CadastroContatosDB;

-- Criando a tabela de contatos
CREATE TABLE IF NOT EXISTS Contatos (
    ID INT AUTO_INCREMENT PRIMARY KEY, -- ID único para cada contato
    Nome VARCHAR(100) NOT NULL,        
    Telefone VARCHAR(20),              
    Email VARCHAR(100),               
    Endereco TEXT                      
);

DELIMITER $$

CREATE PROCEDURE SP_CadastrarContato (
    IN p_Nome VARCHAR(100),
    IN p_Telefone VARCHAR(20),
    IN p_Email VARCHAR(100),
    IN p_Endereco TEXT
)
BEGIN
    INSERT INTO Contatos (Nome, Telefone, Email, Endereco)
    VALUES (p_Nome, p_Telefone, p_Email, p_Endereco);
END$$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE SP_BuscarContatoPorNome (
    IN p_Nome VARCHAR(100)
)
BEGIN
    SELECT * FROM Contatos
    WHERE Nome LIKE CONCAT('%', p_Nome, '%');
END$$

DELIMITER ;

DELIMITER $$

CREATE PROCEDURE SP_AlterarContato (
    IN p_ID INT,
    IN p_Nome VARCHAR(100),
    IN p_Telefone VARCHAR(20),
    IN p_Email VARCHAR(100),
    IN p_Endereco TEXT
)
BEGIN
    UPDATE Contatos
    SET Nome = p_Nome,
        Telefone = p_Telefone,
        Email = p_Email,
        Endereco = p_Endereco
    WHERE ID = p_ID;
END$$

DELIMITER ;

CREATE TABLE IF NOT EXISTS UsuariosLogin (
    UsuarioID INT AUTO_INCREMENT PRIMARY KEY, -- ID único para cada usuário
    NomeUsuario VARCHAR(50) NOT NULL UNIQUE,  -- Nome de usuário (único)
    Senha VARCHAR(50) NOT NULL                -- Senha do usuário
);

INSERT INTO UsuariosLogin (NomeUsuario, Senha)
VALUES ('admin', 'admin123');

-- Inserindo contatos
INSERT INTO Contatos (Nome, Telefone, Email, Endereco) VALUES 
('João Silva', '1234-5678', 'joao.silva@email.com', 'Rua A, 123, Bairro X'),
('Maria Oliveira', '9876-5432', 'maria.oliveira@email.com', 'Rua B, 456, Bairro Y'),
('Carlos Souza', '1234-8765', 'carlos.souza@email.com', 'Avenida C, 789, Bairro Z'),
('Ana Costa', '9876-4321', 'ana.costa@email.com', 'Travessa D, 101, Bairro W'),
('Luiz Pereira', '5555-1234', 'luiz.pereira@email.com', 'Praça E, 202, Bairro V');

-- Stored Procedure para validar login
DELIMITER //
CREATE PROCEDURE sp_ValidarLogin(
    IN p_NomeUsuario VARCHAR(50),
    IN p_Senha VARCHAR(50)
)
BEGIN
    SELECT UsuarioID FROM UsuariosLogin
    WHERE NomeUsuario = p_NomeUsuario AND Senha = p_Senha;
END //
DELIMITER ;