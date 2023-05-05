import React from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import authService from '../services/authService';
import { useState } from 'react';

const RegisterForm = () => {

    const navigate = useNavigate();
    const [isSubmitted, setIsSubmitted] = useState();

    const schema = Yup.object().shape({
        username: Yup.string().required(),
        password: Yup.string().required()
            .matches(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/, 'Password should contains a lowercase, a uppercase character and a digit.')
            .min(6, 'Must be at least 6 symbols')
    });

    const { register, handleSubmit, formState: { errors, isDirty, isValid }, setError } = useForm({
        mode: 'all',
        resolver: yupResolver(schema)
    });

    const handleValidSubmit = async (data) => {
        setIsSubmitted(true)
        try {
            const result = await authService.register(data);

            if (result.status == 200) {
                navigate('/profile');
            }
        } catch (error) {
            console.log(error);

            if (error.data) {
                Object.keys(error.data).forEach((key) => {
                    setError("server", {
                        type: "server",
                        message: error.data[key].join(" "),
                    });
                });
            } else {
                setError("server", {
                    type: "server",
                    message: JSON.stringify(error),
                });

            }
        }
        setIsSubmitted(false)
    }

    return (
        <div className="row">
            <div className="col-6 offset-3">
                <form onSubmit={handleSubmit(handleValidSubmit)}>
                    <div className="mb-3">
                        <label htmlFor="inputUserName" className="form-label">UserName (login)</label>
                        <input type="name" className="form-control" id="inputUserName" {...register('username')} />
                        <div className="form-text text-danger">
                            {errors.username && <p>{errors.username.message}</p>}
                        </div>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="inputPassword" className="form-label">Password</label>
                        <input type="password" className="form-control" id="inputPassword" {...register('password')} />
                        <div className="form-text text-danger">
                            {errors.password && <p>{errors.password.message}</p>}
                        </div>
                    </div>
                    <div className="form-text text-danger">
                        {errors.server && <p>{errors.server.message}</p>}
                    </div>
                    <button type="submit" className="btn btn-primary" disabled={isSubmitted || !isDirty || !isValid}>Submit</button>
                </form>
            </div>
        </div>
    )
}

export default RegisterForm