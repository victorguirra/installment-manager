import { createContext, useContext, useState, type ReactNode } from "react";
import { useNavigate } from "react-router-dom";

import { handleLogin, handleRegister } from "@/services/authService";

interface IProps {
    children: ReactNode
}

interface IAuthContextType {
    isAuthenticated: boolean;
    login: (email: string, password: string) => Promise<void>;
    register: (name: string, email: string, password: string) => Promise<void>;
    logout: () => void;
}

const AuthContext = createContext<IAuthContextType>({} as IAuthContextType);

const accessTokenLocalStorageKey = "ACCESS_TOKEN";

export const AuthProvider = ({ children } : IProps) => {
    const navigate = useNavigate();

    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(() => {
		return !!localStorage.getItem(accessTokenLocalStorageKey);
	});

    const login = async (username: string, password: string) => {
        try{
            const response = await handleLogin(username, password);
            const accessToken = response.accessToken;

            setIsAuthenticated(true);
            localStorage.setItem(accessTokenLocalStorageKey, accessToken);
            navigate('/contracts');
        }
        catch{
            throw new Error('Usuário ou senha inválidos!');
        }
    }

    const register = async (name: string, username: string, password: string) => {
        try{
            const response = await handleRegister(name, username, password);
            const accessToken = response.accessToken;

            setIsAuthenticated(true);
            localStorage.setItem(accessTokenLocalStorageKey, accessToken);
            navigate('/contracts');
        }
        catch{
            throw new Error('Ocorreu um erro ao criar conta!');
        }
    }

    const logout = () => {
        localStorage.removeItem(accessTokenLocalStorageKey);
        navigate('/login');
    };

    return (
        <AuthContext.Provider
        value={{
            isAuthenticated,
            login,
            register,
            logout,
        }}
        >
        {children}
        </AuthContext.Provider>
    );
}

export const useAuth = () => {
    const context = useContext(AuthContext);
  
    if (!context)
        throw new Error("useAuth deve ser usado dentro de um AuthProvider");
  
    return context;
};